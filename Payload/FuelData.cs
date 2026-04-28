using GameReaderCommon;
using Newtonsoft.Json;

namespace SimHub.MQTTPublisher.Payload
{
    public class FuelData
    {
        public FuelData(GameData data, SimHubMQTTPublisherPluginSettings settings)
        {
            if (settings.Include_Fuel)
            {
                Fuel = TelemetryHelper.GetDouble(data, "Fuel");
                FuelLevel = TelemetryHelper.GetDouble(data, "FuelLevel");
            }

            if (settings.Include_FuelCapacity)
                FuelCapacity = TelemetryHelper.GetDouble(data, "MaxFuel");

            if (settings.Include_FuelPerLap)
                FuelPerLap = TelemetryHelper.GetDouble(data, "FuelPerLap");

            // FuelRemaining is intentionally an explicit alias for Fuel (remaining liters),
            // allowing consumers to request only this field without enabling Include_Fuel.
            if (settings.Include_FuelRemaining)
                FuelRemaining = TelemetryHelper.GetDouble(data, "Fuel");

            if (settings.Include_FuelEstimatedLaps)
                FuelEstimatedLaps = TelemetryHelper.GetDouble(data, "EstimatedFuelRemaingLaps");

            if (settings.Include_FuelToEnd)
                FuelToEnd = TelemetryHelper.GetDouble(data, "FuelToEnd");

            if (settings.Include_ERS_Data)
            {
                ERS_DeployedThisLap = TelemetryHelper.GetDouble(data, "ERS_DeployedThisLap");
                ERS_EnergyStore = TelemetryHelper.GetDouble(data, "ERS_EnergyStore");
                ERS_MaxEnergyPerLap = TelemetryHelper.GetDouble(data, "ERS_MaxEnergyPerLap");
                ERSStored = TelemetryHelper.GetDouble(data, "ERSStored");
            }

            if (settings.Include_DRS_Data)
            {
                DRS_Available = TelemetryHelper.GetBool(data, "DRS_Available");
                DRS_Enabled = TelemetryHelper.GetBool(data, "DRS_Enabled");
                DRS_Count = TelemetryHelper.GetInt(data, "DRS_Count");
            }

            if (settings.Include_BatteryData)
            {
                BatteryLevel = TelemetryHelper.GetDouble(data, "BatteryLevel");
                BatteryTemperature = TelemetryHelper.GetDouble(data, "BatteryTemperature");
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Fuel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? FuelLevel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? FuelCapacity { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? FuelPerLap { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? FuelRemaining { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? FuelEstimatedLaps { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? FuelToEnd { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? ERS_DeployedThisLap { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? ERS_EnergyStore { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? ERS_MaxEnergyPerLap { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? ERSStored { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? DRS_Available { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? DRS_Enabled { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? DRS_Count { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? BatteryLevel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? BatteryTemperature { get; set; }
    }
}
