using GameReaderCommon;
using Newtonsoft.Json;

namespace SimHub.MQTTPublisher.Payload
{
    public class TireData
    {
        public TireData(GameData data, SimHubMQTTPublisherPluginSettings settings)
        {
            // SimHub uses British spelling "Tyre" internally
            if (settings.Include_TireTemperatures)
            {
                TemperatureFL = TelemetryHelper.GetDouble(data, "TyreTemperatureFrontLeft");
                TemperatureFR = TelemetryHelper.GetDouble(data, "TyreTemperatureFrontRight");
                TemperatureRL = TelemetryHelper.GetDouble(data, "TyreTemperatureRearLeft");
                TemperatureRR = TelemetryHelper.GetDouble(data, "TyreTemperatureRearRight");
            }

            if (settings.Include_TirePressures)
            {
                PressureFL = TelemetryHelper.GetDouble(data, "TyrePressureFrontLeft");
                PressureFR = TelemetryHelper.GetDouble(data, "TyrePressureFrontRight");
                PressureRL = TelemetryHelper.GetDouble(data, "TyrePressureRearLeft");
                PressureRR = TelemetryHelper.GetDouble(data, "TyrePressureRearRight");
            }

            if (settings.Include_TireWear)
            {
                WearFL = TelemetryHelper.GetDouble(data, "TyreWearFrontLeft");
                WearFR = TelemetryHelper.GetDouble(data, "TyreWearFrontRight");
                WearRL = TelemetryHelper.GetDouble(data, "TyreWearRearLeft");
                WearRR = TelemetryHelper.GetDouble(data, "TyreWearRearRight");
            }

            if (settings.Include_TireGrip)
            {
                GripFL = TelemetryHelper.GetDouble(data, "TyreGripFL");
                GripFR = TelemetryHelper.GetDouble(data, "TyreGripFR");
                GripRL = TelemetryHelper.GetDouble(data, "TyreGripRL");
                GripRR = TelemetryHelper.GetDouble(data, "TyreGripRR");
            }

            if (settings.Include_TireCompound)
            {
                Compound = TelemetryHelper.GetString(data, "TyreCompound");
                CompoundShort = TelemetryHelper.GetString(data, "TyreCompoundShort");
            }

            if (settings.Include_TireDirt)
            {
                DirtFL = TelemetryHelper.GetDouble(data, "TyreDirtFrontLeft");
                DirtFR = TelemetryHelper.GetDouble(data, "TyreDirtFrontRight");
                DirtRL = TelemetryHelper.GetDouble(data, "TyreDirtRearLeft");
                DirtRR = TelemetryHelper.GetDouble(data, "TyreDirtRearRight");
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TemperatureFL { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TemperatureFR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TemperatureRL { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TemperatureRR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? PressureFL { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? PressureFR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? PressureRL { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? PressureRR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? WearFL { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? WearFR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? WearRL { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? WearRR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? GripFL { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? GripFR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? GripRL { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? GripRR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Compound { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CompoundShort { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? DirtFL { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? DirtFR { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? DirtRL { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? DirtRR { get; set; }
    }
}
