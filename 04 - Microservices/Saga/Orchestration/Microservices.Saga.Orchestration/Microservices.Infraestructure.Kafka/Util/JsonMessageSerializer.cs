using Confluent.Kafka;
using Microservices.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Kafka.Util
{
    public class JsonMessageSerializer<Message> : ISerializer<Message>
    {
        private JsonSerializerOptions _options;

        public JsonMessageSerializer()
        {
            _options = new JsonSerializerOptions
            {
                Converters = { new MessageConverter() },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public byte[] Serialize(Message data, SerializationContext context)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data, _options);
        }
    }
}
