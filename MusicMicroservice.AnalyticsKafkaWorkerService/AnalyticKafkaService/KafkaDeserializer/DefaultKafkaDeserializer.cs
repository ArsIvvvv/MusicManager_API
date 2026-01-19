using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace AnalyticKafkaService.KafkaDeserializer
{
    public class DefaultKafkaDeserializer<TMessage>: IDeserializer<TMessage>
    {
        public TMessage Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return JsonSerializer.Deserialize<TMessage>(data)!;
        }
    }
}