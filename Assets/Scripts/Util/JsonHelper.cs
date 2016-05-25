using FullSerializer;

namespace Utils
{
    public class JsonHelper
    {
        private static readonly fsSerializer _serializer = new fsSerializer();

        public static string Serialize<T>(object value, bool pretty = true, bool warnings = true)
        {
            fsData data;
            if (warnings) _serializer.TrySerialize(typeof (T), value, out data).AssertSuccess();
            else _serializer.TrySerialize(typeof (T), value, out data).AssertSuccessWithoutWarnings();

            if (pretty) return fsJsonPrinter.PrettyJson(data);
            else return fsJsonPrinter.CompressedJson(data);
        }

        public static T Deserialize<T>(string serializedState, bool warnings = true)
        {
            fsData data = fsJsonParser.Parse(serializedState);
            object deserialized = null;
            if (warnings) _serializer.TryDeserialize(data, typeof (T), ref deserialized).AssertSuccess();
            else _serializer.TryDeserialize(data, typeof (T), ref deserialized).AssertSuccessWithoutWarnings();
            
            return (T)deserialized;
        }

    }
}