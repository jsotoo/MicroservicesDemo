using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microservices.Messages.Base;
using System.Text.Json.Nodes;

namespace Microservices.Infrastructure.Kafka.Util
{
    public class MessageConverter : JsonConverter<Message>
    {
        public override Message Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Deserialization is not supported.");
        }

        public override void Write(Utf8JsonWriter writer, Message value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("messageType", value.GetType().Name);
            var properties = value.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(value);                
                writer.WritePropertyName(property.Name);
                if (propertyValue != null)
                {
                    JsonSerializer.Serialize(writer, propertyValue, propertyValue.GetType(), options);
                }
                else
                {                 
                    writer.WriteNullValue();
                }

            }
            
            writer.WriteEndObject();

            //JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
