using GameReaderCommon;
using Newtonsoft.Json;

namespace SimHub.MQTTPublisher.Payload
{
    public class DamageData
    {
        public DamageData(GameData data, SimHubMQTTPublisherPluginSettings settings)
        {
            if (settings.Include_CarDamage)
            {
                Engine = TelemetryHelper.GetDouble(data, "CarDamagesEngine");
                Transmission = TelemetryHelper.GetDouble(data, "CarDamagesTransmission");
                Aerodynamics = TelemetryHelper.GetDouble(data, "CarDamagesAero");
                Suspension = TelemetryHelper.GetDouble(data, "CarDamagesSuspension");
                Brakes = TelemetryHelper.GetDouble(data, "CarDamagesBrakes");
                Clutch = TelemetryHelper.GetDouble(data, "CarDamagesClutch");
            }

            if (settings.Include_EngineTemperatures)
            {
                WaterTemperature = TelemetryHelper.GetDouble(data, "WaterTemperature");
                OilTemperature = TelemetryHelper.GetDouble(data, "OilTemperature");
                OilPressure = TelemetryHelper.GetDouble(data, "OilPressure");
                EngineTemperature = TelemetryHelper.GetDouble(data, "EngineTemperature");
            }

            if (settings.Include_BrakeTemperatures)
            {
                BrakeTemperatureFL = TelemetryHelper.GetDouble(data, "BrakeTemperatureFL");
                BrakeTemperatureFR = TelemetryHelper.GetDouble(data, "BrakeTemperatureFR");
                BrakeTemperatureRL = TelemetryHelper.GetDouble(data, "BrakeTemperatureRL");
                BrakeTemperatureRR = TelemetryHelper.GetDouble(data, "BrakeTemperatureRR");
            }

            if (settings.Include_TurboData)
            {
                TurboBoost = TelemetryHelper.GetDouble(data, "TurboBoost");
                Manifold = TelemetryHelper.GetDouble(data, "Manifold");
                ExhaustTemperature = TelemetryHelper.GetDouble(data, "ExhaustTemperature");
            }

            if (settings.Include_WearIndicators)
            {
                EngineWear = TelemetryHelper.GetDouble(data, "EngineWear");
                GearboxWear = TelemetryHelper.GetDouble(data, "GearboxWear");
                SuspensionWear = TelemetryHelper.GetDouble(data, "SuspensionWear");
            }
        }

        // 0-1 range where 1 = fully damaged
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Engine { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Transmission { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Aerodynamics { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Suspension { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Brakes { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Clutch { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? WaterTemperature { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? OilTemperature { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? OilPressure { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? EngineTemperature { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? BrakeTemperatureFL { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? BrakeTemperatureFR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? BrakeTemperatureRL { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? BrakeTemperatureRR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TurboBoost { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Manifold { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? ExhaustTemperature { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? EngineWear { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? GearboxWear { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? SuspensionWear { get; set; }
    }
}
