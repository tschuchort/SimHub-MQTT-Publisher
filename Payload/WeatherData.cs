using GameReaderCommon;
using Newtonsoft.Json;

namespace SimHub.MQTTPublisher.Payload
{
    public class WeatherData
    {
        public WeatherData(GameData data, SimHubMQTTPublisherPluginSettings settings)
        {
            if (settings.Include_AirTemperature)
                AirTemperature = TelemetryHelper.GetDouble(data, "AirTemperature");

            if (settings.Include_TrackTemperature)
            {
                TrackTemperature = TelemetryHelper.GetDouble(data, "TrackTemperature");
                RoadTemperature = TelemetryHelper.GetDouble(data, "RoadTemperature");
            }

            if (settings.Include_WeatherType)
            {
                WeatherType = TelemetryHelper.GetString(data, "WeatherType");
                IsWetTrack = TelemetryHelper.GetBool(data, "IsWetTrack");
            }

            if (settings.Include_RainLevel)
                RainLevel = TelemetryHelper.GetDouble(data, "RainLevel");

            if (settings.Include_Humidity)
                Humidity = TelemetryHelper.GetDouble(data, "Humidity");

            if (settings.Include_WindData)
            {
                WindSpeed = TelemetryHelper.GetDouble(data, "WindSpeed");
                WindDirection = TelemetryHelper.GetDouble(data, "WindDirection");
            }

            if (settings.Include_TrackGrip)
            {
                TrackGrip = TelemetryHelper.GetDouble(data, "TrackGrip");
                TrackWetness = TelemetryHelper.GetDouble(data, "TrackWetness");
            }

            if (settings.Include_TimeOfDay)
            {
                TimeOfDay = TelemetryHelper.GetString(data, "TimeOfDay");
                DayTime = TelemetryHelper.GetDouble(data, "DayTime");
                IsNight = TelemetryHelper.GetBool(data, "IsNight");
                SunAngle = TelemetryHelper.GetDouble(data, "SunAngle");
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? AirTemperature { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TrackTemperature { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? RoadTemperature { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string WeatherType { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? RainLevel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Humidity { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsWetTrack { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? WindSpeed { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? WindDirection { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TrackGrip { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TrackWetness { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TimeOfDay { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? DayTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsNight { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? SunAngle { get; set; }
    }
}
