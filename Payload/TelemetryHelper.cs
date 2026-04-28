using GameReaderCommon;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace SimHub.MQTTPublisher.Payload
{
    internal static class TelemetryHelper
    {
        private static readonly ConcurrentDictionary<Tuple<Type, string>, PropertyInfo> _cache
            = new ConcurrentDictionary<Tuple<Type, string>, PropertyInfo>();

        private static PropertyInfo GetCachedProperty(object target, string name)
        {
            var key = Tuple.Create(target.GetType(), name);
            return _cache.GetOrAdd(key, k => k.Item1.GetProperty(k.Item2));
        }

        public static object GetRaw(GameData data, string name)
        {
            try { return GetCachedProperty(data.NewData, name)?.GetValue(data.NewData); }
            catch { return null; }
        }

        public static double? GetDouble(GameData data, string name)
        {
            try
            {
                var v = GetCachedProperty(data.NewData, name)?.GetValue(data.NewData);
                if (v == null) return null;
                if (v is TimeSpan ts) return ts.TotalMilliseconds;
                if (v is double d) return d;
                if (v is float f) return f;
                if (double.TryParse(v.ToString(), System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double r)) return r;
                return null;
            }
            catch { return null; }
        }

        public static int? GetInt(GameData data, string name)
        {
            try
            {
                var v = GetCachedProperty(data.NewData, name)?.GetValue(data.NewData);
                if (v == null) return null;
                if (v is int i) return i;
                if (int.TryParse(v.ToString(), out int r)) return r;
                return null;
            }
            catch { return null; }
        }

        public static string GetString(GameData data, string name)
        {
            try { return GetCachedProperty(data.NewData, name)?.GetValue(data.NewData)?.ToString(); }
            catch { return null; }
        }

        public static bool? GetBool(GameData data, string name)
        {
            try
            {
                var v = GetCachedProperty(data.NewData, name)?.GetValue(data.NewData);
                if (v == null) return null;
                if (v is bool b) return b;
                if (v is int i) return i != 0;
                if (bool.TryParse(v.ToString(), out bool r)) return r;
                return null;
            }
            catch { return null; }
        }
    }
}
