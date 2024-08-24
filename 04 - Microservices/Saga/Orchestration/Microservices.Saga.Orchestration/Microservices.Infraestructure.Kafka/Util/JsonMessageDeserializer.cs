using Confluent.Kafka;
using Microservices.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Kafka.Util
{
    public class JsonMessageDeserializer : IDeserializer<Message>
    {
        private readonly Dictionary<string, Type> _typeMap;

        public JsonMessageDeserializer()
        {
            Assembly messageAssembly = typeof(Message).Assembly;

            _typeMap = messageAssembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Message)) && !type.IsAbstract)
                .ToDictionary(type => type.Name, type => type);
        }

        public Message Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull) return null;

            var jsonString = Encoding.UTF8.GetString(data);
            var jsonDoc = JsonDocument.Parse(jsonString);
            var root = jsonDoc.RootElement;

            if (root.TryGetProperty("messageType", out var typeElement))
            {
                var typeName = typeElement.GetString();
                if (_typeMap.TryGetValue(typeName, out var type))
                {
                    return (Message)JsonSerializer.Deserialize(jsonString, type, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                throw new JsonException($"Unknown messageType: {typeName}");
            }

            throw new JsonException("MessageType property is missing in the JSON.");
        }
    }
}
