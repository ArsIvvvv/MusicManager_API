using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using MusicMicroservice.Application.Common.Interfaces.Persistance.Kafka;
using MusicMicroservice.Application.Common.Settings;
using MusicMicroservice.Contracts.Responses.Music;
using MusicMicroservice.Domain.Entities;
using MusicMicroservice.Infrastructure.Kafka.Serializer;

namespace MusicMicroservice.Infrastructure.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly string _topic;
        private readonly IProducer<string, MusicResponse> _producer;

        public KafkaProducer(IOptions<KafkaSettings> kafkaSettings)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.Value.BootstrapServers
            };

            _producer = new ProducerBuilder<string, MusicResponse>(config)
                .SetValueSerializer(new KafkaSerializer<MusicResponse>())
                .Build();

            _topic = kafkaSettings.Value.Topic;
        }

        public async Task ProduceAsync<T>(MusicResponse response, CancellationToken cancellationToken = default)
        {
           await _producer.ProduceAsync(_topic, new Message<string, MusicResponse>
           {
               Key = response.id.ToString(),
               Value = response
           }, 
           cancellationToken);
        }
        public void Dispose()
        {
            _producer?.Dispose();
        }
    }
}