using GameReaderCommon;
using Newtonsoft.Json;

namespace SimHub.MQTTPublisher.Payload
{
    public class InputData
    {
        public InputData(GameData data, SimHubMQTTPublisherPluginSettings settings)
        {
            if (settings.Include_SteeringInput)
            {
                SteeringAngle = TelemetryHelper.GetDouble(data, "SteeringAngle");
                SteeringInput = TelemetryHelper.GetDouble(data, "SteeringInput");
                SteeringWheelAngle = TelemetryHelper.GetDouble(data, "SteeringWheelAngle");
            }

            if (settings.Include_PedalInputs)
            {
                ThrottleRaw = TelemetryHelper.GetDouble(data, "ThrottleRaw");
                BrakeRaw = TelemetryHelper.GetDouble(data, "BrakeRaw");
                ClutchRaw = TelemetryHelper.GetDouble(data, "ClutchRaw");
                Handbrake = TelemetryHelper.GetDouble(data, "Handbrake");
                PitLimiter = TelemetryHelper.GetBool(data, "PitLimiter");
            }

            if (settings.Include_DriverAssists)
            {
                TractionControl = TelemetryHelper.GetInt(data, "TractionControl");
                TractionControlLevel = TelemetryHelper.GetInt(data, "TractionControlLevel");
                ABS = TelemetryHelper.GetInt(data, "ABS");
                ABSLevel = TelemetryHelper.GetInt(data, "ABSLevel");
                StabilityControl = TelemetryHelper.GetInt(data, "StabilityControl");
                AutoClutch = TelemetryHelper.GetBool(data, "AutoClutch");
                AutoGear = TelemetryHelper.GetBool(data, "AutoGear");
            }

            if (settings.Include_ElectronicSystems)
            {
                ElectronicStabilityProgram = TelemetryHelper.GetBool(data, "ElectronicStabilityProgram");
                BrakeBias = TelemetryHelper.GetDouble(data, "BrakeBias");
                TractionControlCut = TelemetryHelper.GetDouble(data, "TractionControlCut");
            }

            if (settings.Include_InputDeviceInfo)
            {
                IsKeyboard = TelemetryHelper.GetBool(data, "IsKeyboard");
                IsGamepad = TelemetryHelper.GetBool(data, "IsGamepad");
                IsWheel = TelemetryHelper.GetBool(data, "IsWheel");
            }
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? SteeringAngle { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? SteeringInput { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? SteeringWheelAngle { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? ThrottleRaw { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? BrakeRaw { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? ClutchRaw { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Handbrake { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? PitLimiter { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TractionControl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? TractionControlLevel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ABS { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ABSLevel { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? StabilityControl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? AutoClutch { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? AutoGear { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? ElectronicStabilityProgram { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? BrakeBias { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? TractionControlCut { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsKeyboard { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsGamepad { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsWheel { get; set; }
    }
}
