using GameReaderCommon;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using SimHub.MQTTPublisher.Settings;
using SimHub.Plugins;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SimHub.MQTTPublisher
{
    [PluginDescription("MQTT Publisher")]
    [PluginAuthor("TSchuchort")]
    [PluginName("MQTT Publisher Enhanced")]
    public class SimHubMQTTPublisherPlugin : IPlugin, IDataPlugin, IWPFSettingsV2
    {
        public SimHubMQTTPublisherPluginSettings Settings;
        public SimHubMQTTPublisherPluginUserSettings UserSettings { get; private set; }

        private MqttFactory mqttFactory;
        private IMqttClient mqttClient;
        private readonly Stopwatch _publishStopwatch = new Stopwatch();
        private string _lastPublishedPayload;

        // Guards against concurrent CreateMQTTClient calls (e.g. rapid Apply Settings clicks)
        private volatile bool _isCreatingClient;

        // Cancelled when the associated mqtt client is replaced or the plugin ends,
        // stopping its reconnect loop.
        private CancellationTokenSource _reconnectCts;

        public PluginManager PluginManager { get; set; }

        public ImageSource PictureIcon => this.ToIcon(Properties.Resources.sdkmenuicon);

        public string LeftMenuTitle => "MQTT Publisher";

        public void DataUpdate(PluginManager pluginManager, ref GameData data)
        {
            if (!data.GameRunning)
                return;

            var client = mqttClient;
            if (client == null || !client.IsConnected)
                return;

            if (_publishStopwatch.IsRunning && _publishStopwatch.ElapsedMilliseconds < Settings.UpdateIntervalMs)
                return;

            _publishStopwatch.Restart();

            var payloadRoot = new Payload.PayloadRoot(data, UserSettings, Settings);

            var payload = JsonConvert.SerializeObject(
                payloadRoot,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            var payloadForChangeDetection = BuildChangeDetectionPayload(payloadRoot, payload);

            if (Settings.PublishOnChangeOnly && payloadForChangeDetection == _lastPublishedPayload)
                return;

            _lastPublishedPayload = payloadForChangeDetection;

            var resolvedTopic = ResolveTopic(Settings.Topic, data);

            var applicationMessage = new MqttApplicationMessageBuilder()
               .WithTopic(resolvedTopic)
               .WithPayload(payload)
               .Build();

            // Fire-and-forget: log failures without blocking the critical DataUpdate path
            var publishTask = client.PublishAsync(applicationMessage, CancellationToken.None);
            publishTask.ContinueWith(
                t => SimHub.Logging.Current.Warn($"MQTT publish failed: {t.Exception?.GetBaseException()?.Message}"),
                TaskContinuationOptions.OnlyOnFaulted);
        }

        private string BuildChangeDetectionPayload(Payload.PayloadRoot payloadRoot, string payload)
        {
            if (!Settings.PublishOnChangeOnly || !Settings.Include_Time)
                return payload;

            // Exclude timestamp from change comparison so a timer tick alone doesn't trigger publish
            var originalTime = payloadRoot.time;
            payloadRoot.time = null;

            var payloadWithoutTime = JsonConvert.SerializeObject(
                payloadRoot,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            payloadRoot.time = originalTime;
            return payloadWithoutTime;
        }

        public void End(PluginManager pluginManager)
        {
            this.SaveCommonSettings("GeneralSettings", Settings);
            this.SaveCommonSettings("UserSettings", UserSettings);

            _reconnectCts?.Cancel();
            _reconnectCts?.Dispose();
            _reconnectCts = null;

            mqttClient?.Dispose();
        }

        public System.Windows.Controls.Control GetWPFSettingsControl(PluginManager pluginManager)
        {
            return new SimHubMQTTPublisherPluginUI(this);
        }

        public void Init(PluginManager pluginManager)
        {
            SimHub.Logging.Current.Info("Starting plugin");

            Settings = this.ReadCommonSettings<SimHubMQTTPublisherPluginSettings>(
                "GeneralSettings", () => new SimHubMQTTPublisherPluginSettings());

            UserSettings = this.ReadCommonSettings<SimHubMQTTPublisherPluginUserSettings>(
                "UserSettings", () => new SimHubMQTTPublisherPluginUserSettings());

            mqttFactory = new MqttFactory();

            Task.Run(() => CreateMQTTClient());
        }

        internal void CreateMQTTClient()
        {
            // Prevent concurrent calls from e.g. rapid Apply Settings clicks
            if (_isCreatingClient)
                return;
            _isCreatingClient = true;

            try
            {
                // Cancel the reconnect loop of the previous client
                _reconnectCts?.Cancel();
                _reconnectCts?.Dispose();

                var cts = new CancellationTokenSource();
                _reconnectCts = cts;

                var newClient = mqttFactory.CreateMqttClient();
                var options = new MqttClientOptionsBuilder()
                   .WithTcpServer(Settings.Server)
                   .WithCredentials(Settings.Login, Settings.Password)
                   .Build();

                // Reconnect automatically whenever this client loses its connection,
                // unless the CTS has been cancelled (plugin ending or settings reapplied).
                newClient.UseDisconnectedHandler(async e =>
                {
                    if (cts.IsCancellationRequested)
                        return;

                    SimHub.Logging.Current.Info("MQTT disconnected, reconnecting in 5 s…");

                    try
                    {
                        await Task.Delay(5000, cts.Token).ConfigureAwait(false);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }

                    if (cts.IsCancellationRequested)
                        return;

                    try
                    {
                        await newClient.ConnectAsync(options, cts.Token).ConfigureAwait(false);
                        SimHub.Logging.Current.Info("MQTT reconnected.");
                    }
                    catch (OperationCanceledException) { }
                    catch (Exception ex)
                    {
                        SimHub.Logging.Current.Warn($"MQTT reconnect failed: {ex.Message}");
                    }
                });

                // Replace the active client atomically and dispose the old one
                var oldClient = mqttClient;
                mqttClient = newClient;
                oldClient?.Dispose();

                // Initial connection attempt (10-second timeout)
                try
                {
                    using (var connectCts = new CancellationTokenSource(TimeSpan.FromSeconds(10)))
                    {
                        newClient.ConnectAsync(options, connectCts.Token).GetAwaiter().GetResult();
                    }
                }
                catch (Exception ex)
                {
                    SimHub.Logging.Current.Warn($"MQTT connect failed: {ex.Message}");
                    // DisconnectedHandler will retry automatically
                }
            }
            finally
            {
                _isCreatingClient = false;
            }
        }

        private string ResolveTopic(string topic, GameData data)
        {
            if (string.IsNullOrEmpty(topic))
                return topic;

            if (topic.Contains("{gameName}"))
                topic = topic.Replace("{gameName}", SanitizeTopicSegment(data.GameName ?? "Unknown"));

            if (topic.Contains("{sessionType}"))
                topic = topic.Replace("{sessionType}", SanitizeTopicSegment(
                    GetPropertyValue(data, "SessionTypeName") ?? "Unknown"));

            if (topic.Contains("{trackName}"))
                topic = topic.Replace("{trackName}", SanitizeTopicSegment(
                    GetPropertyValue(data, "TrackName") ?? GetPropertyValue(data, "TrackDisplayName") ?? "Unknown"));

            if (topic.Contains("{carName}"))
                topic = topic.Replace("{carName}", SanitizeTopicSegment(
                    GetPropertyValue(data, "CarName") ?? GetPropertyValue(data, "CarModel") ?? "Unknown"));

            return topic;
        }

        private string GetPropertyValue(GameData data, string propertyName)
        {
            try
            {
                var property = data.NewData.GetType().GetProperty(propertyName);
                return property?.GetValue(data.NewData)?.ToString();
            }
            catch
            {
                return null;
            }
        }

        private string SanitizeTopicSegment(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "Unknown";

            input = input.Replace(" ", "_");
            input = System.Text.RegularExpressions.Regex.Replace(input, @"[^a-zA-Z0-9_\-]", "");

            return string.IsNullOrEmpty(input) ? "Unknown" : input;
        }
    }
}
