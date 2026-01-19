using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnalyticKafkaService.Service.QueueService;

namespace AnalyticKafkaService.Service.HostService
{
    public class QueueHostService : BackgroundService
    {
        private readonly ILogger<QueueHostService> _logger;

        private readonly IBackgroundTaskQueue _taskQueue;

        public QueueHostService(IBackgroundTaskQueue taskQueue, ILogger<QueueHostService> logger)
        {
            _taskQueue = taskQueue;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("HostService is starting.");

            return ProcessTaskQueueAsync(stoppingToken);
        }

        private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var workItem = await _taskQueue.DequeueAsync(stoppingToken);

                    await workItem(stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("HostService is stopping.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing task work item.");
                throw;
            }
        }
    }
}