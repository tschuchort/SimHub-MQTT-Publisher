using GameReaderCommon;
using System;
using System.Collections.Generic;

namespace SimHub.MQTTPublisher.Payload
{
    public class PayloadRoot
    {
        public PayloadRoot(GameData data, SimHubMQTTPublisherPluginUserSettings userSettings, SimHubMQTTPublisherPluginSettings settings)
        {
            if (settings.Include_Time)
                time = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            if (settings.Include_UserId)
                userId = userSettings.UserId.ToString();

            if (settings.Include_GameName)
                gameName = data.GameName ?? "Unknown";

            if (settings.EnableDebugMode)
                debugData = ExtractAllTelemetryData(data);

            if (settings.Include_SpeedKmh || settings.Include_Rpms || settings.Include_Gear ||
                settings.Include_Throttle || settings.Include_Brake || settings.Include_Clutch ||
                settings.Include_CarCoordinates || settings.Include_CurrentLapTime ||
                settings.Include_CarModel || settings.Include_CarClass ||
                settings.Include_EngineIgnitionOn || settings.Include_EngineStarted)
            {
                carState = new Car(data, settings);
            }

            if (settings.Include_Flags || settings.Include_FlagName || settings.Include_DebugFlags)
                flagState = new Flag(data, settings);

            if (settings.Include_TrackName || settings.Include_TrackLength || settings.Include_TrackConfiguration)
                trackInformation = new TrackInformation(data, settings);

            if (settings.Include_VehicleModel || settings.Include_VehicleClass || settings.Include_MaxRpm)
                vehicleInformation = new VehicleInformation(data, settings);

            if (settings.Include_SessionType || settings.Include_SessionTimeLeft || settings.Include_SessionLaps)
                sessionInfo = new SessionInfo(data, settings);

            if (settings.Include_Position || settings.Include_PositionInClass ||
                settings.Include_Gap || settings.Include_GapToLeader ||
                settings.Include_GapToAhead || settings.Include_GapToBehind ||
                settings.Include_LastLapTime || settings.Include_BestLapTime ||
                settings.Include_PersonalBestLapTime || settings.Include_SessionBestLapTime ||
                settings.Include_DeltaToSessionBest || settings.Include_DeltaToPersonalBest ||
                settings.Include_DeltaToOptimal || settings.Include_Sector1Time ||
                settings.Include_Sector2Time || settings.Include_Sector3Time ||
                settings.Include_Sector1BestTime || settings.Include_Sector2BestTime ||
                settings.Include_Sector3BestTime || settings.Include_CurrentSector ||
                settings.Include_CurrentLap || settings.Include_TotalLaps ||
                settings.Include_CompletedLaps)
            {
                positionData = new PositionData(data, settings);
            }

            if (settings.Include_TireTemperatures || settings.Include_TirePressures ||
                settings.Include_TireWear || settings.Include_TireGrip ||
                settings.Include_TireCompound || settings.Include_TireDirt)
            {
                tireData = new TireData(data, settings);
            }

            if (settings.Include_Fuel || settings.Include_FuelCapacity ||
                settings.Include_FuelPerLap || settings.Include_FuelRemaining ||
                settings.Include_FuelEstimatedLaps || settings.Include_FuelToEnd ||
                settings.Include_ERS_Data || settings.Include_DRS_Data ||
                settings.Include_BatteryData)
            {
                fuelData = new FuelData(data, settings);
            }

            if (settings.Include_AirTemperature || settings.Include_TrackTemperature ||
                settings.Include_WeatherType || settings.Include_RainLevel ||
                settings.Include_Humidity || settings.Include_WindData ||
                settings.Include_TrackGrip || settings.Include_TimeOfDay)
            {
                weatherData = new WeatherData(data, settings);
            }

            if (settings.Include_CarDamage || settings.Include_EngineTemperatures ||
                settings.Include_BrakeTemperatures || settings.Include_TurboData ||
                settings.Include_WearIndicators)
            {
                damageData = new DamageData(data, settings);
            }

            if (settings.Include_SteeringInput || settings.Include_PedalInputs ||
                settings.Include_DriverAssists || settings.Include_ElectronicSystems ||
                settings.Include_InputDeviceInfo)
            {
                inputData = new InputData(data, settings);
            }

            if (settings.Include_SafetyCarInfo || settings.Include_FlagSectors ||
                settings.Include_PitInformation || settings.Include_RaceControl ||
                settings.Include_Penalties || settings.Include_FormationLap)
            {
                safetyData = new SafetyData(data, settings);
            }
        }

        public long? time { get; set; }
        public string userId { get; set; }
        public string gameName { get; set; }
        public Car carState { get; set; }
        public Flag flagState { get; set; }
        public TrackInformation trackInformation { get; set; }
        public VehicleInformation vehicleInformation { get; set; }
        public SessionInfo sessionInfo { get; set; }
        public PositionData positionData { get; set; }
        public TireData tireData { get; set; }
        public FuelData fuelData { get; set; }
        public WeatherData weatherData { get; set; }
        public DamageData damageData { get; set; }
        public InputData inputData { get; set; }
        public SafetyData safetyData { get; set; }
        public object debugData { get; set; }

        private Dictionary<string, object> ExtractAllTelemetryData(GameData data)
        {
            var allData = new Dictionary<string, object>();

            try
            {
                allData["_GameName"] = data.GameName ?? "Unknown";
                allData["_GameRunning"] = data.GameRunning;

                var properties = data.NewData.GetType().GetProperties();
                var propertiesData = new Dictionary<string, object>();

                foreach (var prop in properties)
                {
                    try
                    {
                        var value = prop.GetValue(data.NewData);

                        if (prop.PropertyType.IsClass &&
                            prop.PropertyType != typeof(string) &&
                            !prop.PropertyType.IsArray)
                        {
                            propertiesData[prop.Name] = $"[Complex Object: {prop.PropertyType.Name}]";
                            continue;
                        }

                        if (value != null && prop.PropertyType.IsArray)
                        {
                            try
                            {
                                var array = value as Array;
                                var items = new List<string>();
                                foreach (var item in array)
                                    items.Add(item?.ToString() ?? "null");
                                propertiesData[prop.Name] = $"[{string.Join(", ", items)}]";
                            }
                            catch
                            {
                                propertiesData[prop.Name] = "[Array]";
                            }
                            continue;
                        }

                        propertiesData[prop.Name] = value?.ToString() ?? "null";
                    }
                    catch (Exception propEx)
                    {
                        propertiesData[prop.Name] = $"ERROR: {propEx.Message}";
                    }
                }

                allData["AllProperties"] = propertiesData;
                allData["_PropertyCount"] = propertiesData.Count;
            }
            catch (Exception ex)
            {
                allData["_Error"] = $"Failed to extract telemetry: {ex.Message}";
            }

            return allData;
        }
    }
}
