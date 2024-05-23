using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SmartTech.Marketing.Core.Helper
{
    public class TruncateDecimalConverter : JsonConverter
    {
        private readonly int decimalPlaces;

        public TruncateDecimalConverter(int decimalPlaces)
        {
            this.decimalPlaces = decimalPlaces;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal) || objectType == typeof(decimal?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Float)
            {
                if (reader.Value != null)
                {
                    if (decimal.TryParse(reader.Value.ToString(), out decimal value))
                    {
                        var truncatedValue = Math.Round(value, decimalPlaces, MidpointRounding.ToZero);
                        return truncatedValue;
                    }
                }

            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}
