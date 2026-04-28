using GameReaderCommon;
using Newtonsoft.Json;

namespace SimHub.MQTTPublisher.Payload
{
    public class SafetyData
    {
        public SafetyData(GameData data, SimHubMQTTPublisherPluginSettings settings)
        {
            if (settings.Include_SafetyCarInfo)
            {
                SafetyCar = TelemetryHelper.GetBool(data, "SafetyCar");
                SafetyCarActive = TelemetryHelper.GetBool(data, "SafetyCarActive");
                VirtualSafetyCar = TelemetryHelper.GetBool(data, "VirtualSafetyCar");
                SafetyCarTime = TelemetryHelper.GetDouble(data, "SafetyCarTime");
            }

            if (settings.Include_FlagSectors)
            {
                YellowFlagSector1 = TelemetryHelper.GetBool(data, "YellowFlagSector1");
                YellowFlagSector2 = TelemetryHelper.GetBool(data, "YellowFlagSector2");
                YellowFlagSector3 = TelemetryHelper.GetBool(data, "YellowFlagSector3");
            }

            if (settings.Include_PitInformation)
            {
                IsInPitLane = TelemetryHelper.GetBool(data, "IsInPitLane");
                IsInPit = TelemetryHelper.GetBool(data, "IsInPit");
                PitSpeedLimit = TelemetryHelper.GetDouble(data, "PitSpeedLimit");
                PitLimiterOn = TelemetryHelper.GetBool(data, "PitLimiterOn");
                PitWindowStart = TelemetryHelper.GetInt(data, "PitWindowStart");
                PitWindowEnd = TelemetryHelper.GetInt(data, "PitWindowEnd");
                MandatoryPitDone = TelemetryHelper.GetBool(data, "MandatoryPitDone");
            }

            if (settings.Include_RaceControl)
            {
                RaceStarted = TelemetryHelper.GetBool(data, "RaceStarted");
                RaceFinished = TelemetryHelper.GetBool(data, "RaceFinished");
                SessionPaused = TelemetryHelper.GetBool(data, "SessionPaused");
                IsReplay = TelemetryHelper.GetBool(data, "IsReplay");
                IsSpectator = TelemetryHelper.GetBool(data, "IsSpectator");
                RedFlagActive = TelemetryHelper.GetBool(data, "RedFlagActive");
                SessionStopped = TelemetryHelper.GetBool(data, "SessionStopped");
            }

            if (settings.Include_Penalties)
            {
                HasPenalty = TelemetryHelper.GetBool(data, "HasPenalty");
                PenaltyTime = TelemetryHelper.GetDouble(data, "PenaltyTime");
                PenaltyCount = TelemetryHelper.GetInt(data, "PenaltyCount");
            }

            if (settings.Include_FormationLap)
            {
                FormationLap = TelemetryHelper.GetBool(data, "FormationLap");
                WarmupLap = TelemetryHelper.GetBool(data, "WarmupLap");
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? SafetyCar { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? SafetyCarActive { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? VirtualSafetyCar { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? SafetyCarTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? YellowFlagSector1 { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? YellowFlagSector2 { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? YellowFlagSector3 { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsInPitLane { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsInPit { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? PitSpeedLimit { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? PitLimiterOn { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PitWindowStart { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PitWindowEnd { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? MandatoryPitDone { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? RaceStarted { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? RaceFinished { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? SessionPaused { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsReplay { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsSpectator { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasPenalty { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? PenaltyTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PenaltyCount { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? FormationLap { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? WarmupLap { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? RedFlagActive { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? SessionStopped { get; set; }
    }
}
