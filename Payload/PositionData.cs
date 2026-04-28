using GameReaderCommon;
using Newtonsoft.Json;

namespace SimHub.MQTTPublisher.Payload
{
    public class PositionData
    {
        public PositionData(GameData data, SimHubMQTTPublisherPluginSettings settings)
        {
            if (settings.Include_Position)
                Position = TelemetryHelper.GetInt(data, "Position");
            if (settings.Include_PositionInClass)
                PositionInClass = TelemetryHelper.GetInt(data, "PositionInClass");

            if (settings.Include_Gap)
                Gap = TelemetryHelper.GetDouble(data, "Gap");
            if (settings.Include_GapToLeader)
                GapToLeader = TelemetryHelper.GetDouble(data, "GapToLeader");
            if (settings.Include_GapToAhead)
                GapToAhead = TelemetryHelper.GetDouble(data, "GapToAhead");
            if (settings.Include_GapToBehind)
                GapToBehind = TelemetryHelper.GetDouble(data, "GapToBehind");

            // Lap times — TelemetryHelper.GetDouble converts TimeSpan to milliseconds automatically
            if (settings.Include_LastLapTime)
                LastLapTime = TelemetryHelper.GetDouble(data, "LastLapTime");
            if (settings.Include_BestLapTime)
                BestLapTime = TelemetryHelper.GetDouble(data, "BestLapTime");
            if (settings.Include_PersonalBestLapTime)
                PersonalBestLapTime = TelemetryHelper.GetDouble(data, "AllTimeBest");
            if (settings.Include_SessionBestLapTime)
                SessionBestLapTime = TelemetryHelper.GetDouble(data, "SessionBestLapTime");

            if (settings.Include_DeltaToSessionBest)
                DeltaToSessionBest = TelemetryHelper.GetDouble(data, "DeltaToSessionBest");
            if (settings.Include_DeltaToPersonalBest)
                DeltaToPersonalBest = TelemetryHelper.GetDouble(data, "DeltaToAllTimeBest");
            if (settings.Include_DeltaToOptimal)
                DeltaToOptimal = TelemetryHelper.GetDouble(data, "DeltaToOptimal");

            if (settings.Include_Sector1Time)
                Sector1Time = TelemetryHelper.GetDouble(data, "Sector1Time");
            if (settings.Include_Sector2Time)
                Sector2Time = TelemetryHelper.GetDouble(data, "Sector2Time");
            if (settings.Include_Sector3Time)
                Sector3Time = TelemetryHelper.GetDouble(data, "Sector3Time");
            if (settings.Include_Sector1BestTime)
                Sector1BestTime = TelemetryHelper.GetDouble(data, "Sector1BestLapTime");
            if (settings.Include_Sector2BestTime)
                Sector2BestTime = TelemetryHelper.GetDouble(data, "Sector2BestLapTime");
            if (settings.Include_Sector3BestTime)
                Sector3BestTime = TelemetryHelper.GetDouble(data, "Sector3BestLapTime");
            if (settings.Include_CurrentSector)
                CurrentSector = TelemetryHelper.GetInt(data, "CurrentSectorIndex");

            if (settings.Include_CurrentLap)
                CurrentLap = TelemetryHelper.GetInt(data, "CurrentLap");
            if (settings.Include_TotalLaps)
                TotalLaps = TelemetryHelper.GetInt(data, "TotalLaps");
            if (settings.Include_CompletedLaps)
                CompletedLaps = TelemetryHelper.GetInt(data, "CompletedLaps");
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Position { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PositionInClass { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Gap { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? GapToLeader { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? GapToAhead { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? GapToBehind { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? LastLapTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? BestLapTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? PersonalBestLapTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? SessionBestLapTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? DeltaToSessionBest { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? DeltaToPersonalBest { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? DeltaToOptimal { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Sector1Time { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Sector2Time { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Sector3Time { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Sector1BestTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Sector2BestTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Sector3BestTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CurrentSector { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CurrentLap { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TotalLaps { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CompletedLaps { get; set; }
    }
}
