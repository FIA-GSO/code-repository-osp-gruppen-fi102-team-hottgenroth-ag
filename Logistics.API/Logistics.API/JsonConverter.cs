using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Logistics.API
{
    public class JsonConverter<I, T> : JsonConverter
    {
        [DebuggerStepThrough]
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(I));
        }

        [DebuggerStepThrough]
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                JObject jo = JObject.Load(reader);
                return jo.ToObject<T>(serializer);
            }
            catch
            {
                return Activator.CreateInstance<T>();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}