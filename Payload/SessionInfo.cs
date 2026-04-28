using GameReaderCommon;
using Newtonsoft.Json;

namespace SimHub.MQTTPublisher.Payload
{
    public class SessionInfo
    {
        public SessionInfo(GameData data, SimHubMQTTPublisherPluginSettings settings)
        {
            if (settings.Include_SessionType)
                SessionType = TelemetryHelper.GetString(data, "SessionTypeName");

            if (settings.Include_SessionTimeLeft)
                SessionTimeLeft = TelemetryHelper.GetDouble(data, "SessionTimeLeft");

            if (settings.Include_SessionLaps)
                SessionLaps = TelemetryHelper.GetInt(data, "TotalLaps");
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SessionType { get; set; }

        // milliseconds (TimeSpan values are converted automatically by TelemetryHelper)
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? SessionTimeLeft { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? SessionLaps { get; set; }
    }
}
