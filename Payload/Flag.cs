using GameReaderCommon;
using Newtonsoft.Json;

namespace SimHub.MQTTPublisher.Payload
{
    /// <summary>
    /// Flag information for racing simulators.
    ///
    /// iRacing SessionFlags bit values:
    /// 0x00000001 (1)      = Checkered flag
    /// 0x00000002 (2)      = White flag (1 lap to go)
    /// 0x00000004 (4)      = Green flag (start/restart)
    /// 0x00000008 (8)      = Yellow flag (caution)
    /// 0x00000010 (16)     = Red flag (session stopped)
    /// 0x00000020 (32)     = Blue flag (faster car approaching)
    /// 0x00000040 (64)     = Debris flag
    /// 0x00000080 (128)    = Crossed flag
    /// 0x00000100 (256)    = Yellow waving
    /// 0x00000200 (512)    = One lap to green
    /// 0x00000400 (1024)   = Green held
    /// 0x00000800 (2048)   = Ten to go
    /// 0x00001000 (4096)   = Five to go
    /// 0x00002000 (8192)   = Random waving
    /// 0x00004000 (16384)  = Full course caution
    /// 0x00008000 (32768)  = Full course caution waving
    /// 0x00010000 (65536)  = Black flag
    /// 0x00020000 (131072) = Disqualify flag
    ///
    /// Flags can be combined (bitwise OR). Use bitwise AND to check individual flags.
    /// Example: (Flags &amp; 4) != 0 checks for green flag.
    /// </summary>
    public class Flag
    {
        public Flag(GameData data, SimHubMQTTPublisherPluginSettings settings)
        {
            if (settings.Include_Flags)
            {
                Flags = TelemetryHelper.GetInt(data, "SessionFlags")
                        ?? BuildFlagsFromIndividualProperties(data)
                        ?? 0;
            }

            if (settings.Include_FlagName)
                FlagName = TelemetryHelper.GetString(data, "Flag_Name");

            if (settings.Include_DebugFlags)
            {
                DebugFlags = new
                {
                    Flag_Green = TelemetryHelper.GetInt(data, "Flag_Green"),
                    Flag_Yellow = TelemetryHelper.GetInt(data, "Flag_Yellow"),
                    Flag_Red = TelemetryHelper.GetInt(data, "Flag_Red"),
                    Flag_Blue = TelemetryHelper.GetInt(data, "Flag_Blue"),
                    Flag_White = TelemetryHelper.GetInt(data, "Flag_White"),
                    Flag_Black = TelemetryHelper.GetInt(data, "Flag_Black"),
                    Flag_Checkered = TelemetryHelper.GetInt(data, "Flag_Checkered"),
                    Flag_Orange = TelemetryHelper.GetInt(data, "Flag_Orange"),
                    SessionFlags = TelemetryHelper.GetInt(data, "SessionFlags")
                };
            }
        }

        /// <summary>
        /// Combined flag state as integer. Use bitwise operations to check individual flags.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Flags { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string FlagName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object DebugFlags { get; set; }

        private int? BuildFlagsFromIndividualProperties(GameData data)
        {
            int flags = 0;

            if (TelemetryHelper.GetBool(data, "Flag_Checkered") == true) flags |= 0x1;
            if (TelemetryHelper.GetBool(data, "Flag_White")     == true) flags |= 0x2;
            if (TelemetryHelper.GetBool(data, "Flag_Green")     == true) flags |= 0x4;
            if (TelemetryHelper.GetBool(data, "Flag_Yellow")    == true) flags |= 0x8;
            if (TelemetryHelper.GetBool(data, "Flag_Red")       == true) flags |= 0x10;
            if (TelemetryHelper.GetBool(data, "Flag_Blue")      == true) flags |= 0x20;
            if (TelemetryHelper.GetBool(data, "Flag_Orange")    == true) flags |= 0x40;
            if (TelemetryHelper.GetBool(data, "Flag_Black")     == true) flags |= 0x10000;

            return flags == 0 ? (int?)null : flags;
        }
    }
}
