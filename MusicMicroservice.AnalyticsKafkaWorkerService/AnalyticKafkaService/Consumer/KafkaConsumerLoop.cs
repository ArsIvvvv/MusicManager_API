using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalyticKafkaService.Interface;
using AnalyticKafkaService.KafkaDeserializer;
using AnalyticKafkaService.Models;
using AnalyticKafkaService.Service.QueueService;
using AnalyticKafkaService.Settings;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using MusicMicroservice.Contracts.Responses.Music;

namespace AnalyticKafkaService.Consumer
{
    public class KafkaConsumerLoop : IDisposable
    {
        private readonly ILogger<KafkaConsumerLoop> _logger;    

        private readonly IBackgroundTaskQueue _taskQueue;

        private readonly IMusicAnalyticRepository _musicAnalyticRepository;

        private readonly IConsumer<string, MusicResponse> _consumer;
        private readonly string _topic;

        private readonly CancellationToken _cancellationToken;

        public KafkaConsumerLoop(
            ILogger<KafkaConsumerLoop> logger,
            IBackgroundTaskQueue taskQueue,
            IMusicAnalyticRepository musicAnalyticRepository,
            IHostApplicationLifetime appLifetime,
            IOptions<KafkaSettings> options)
        {
            _logger = logger;
            _taskQueue = taskQueue;
            _musicAnalyticRepository = musicAnalyticRepository;
            
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = options.Value.BootstrapServers,
                GroupId = options.Value.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoOffsetStore = false,
                EnableAutoCommit = true
            };

            _consumer = new ConsumerBuilder<string, MusicResponse>(consumerConfig)
                .SetValueDeserializer(new DefaultKafkaDeserializer<MusicResponse>())
                .Build();

            _topic = options.Value.Topic;

            _cancellationToken = appLifetime.ApplicationStopping;
        }

        public void StartConsume()
        {
            _consumer.Subscribe(_topic);
            Task.Run(() => ConsumeLoop(_cancellationToken));
        }

        private async Task ConsumeLoop(CancellationToken cancellationToken)
        {
          try
          {  
                while(!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(cancellationToken);

                        Func<CancellationToken, ValueTask> workItem = async token =>
                        {
                            _logger.LogInformation($"Consumed message (ConsumeLoop Method) at {consumeResult.TopicPartitionOffset} with key: {consumeResult.Message.Key}");
                            var musicData = new MusicData
                            {
                                Id = Guid.NewGuid(),
                                MusicId = consumeResult.Message.Value.id,
                                Name = consumeResult.Message.Value.name,
                                Year = consumeResult.Message.Value.year,
                                Style = consumeResult.Message.Value.style
                            };

                            await _musicAnalyticRepository.AddAsync(musicData, token);

                            _consumer.StoreOffset(consumeResult);

                            _logger.LogInformation($"Added analytics: Music - {musicData.Name}");
                        };

                        await _taskQueue.QueueBackgroundWorkItemAsync(workItem);

                    }
                    catch (OperationCanceledException)
                    {
                        _logger.LogInformation("Kafka consumer loop is stopping.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred in Kafka consumer loop.");
                    }
                }
          }
          finally
          {
            _logger.LogInformation("Closing Kafka consumer for topic {Topic}.", _topic);
            _consumer.Close();
          }
        }

        public void Dispose()
        {
            _consumer?.Dispose();
        }
    }
}