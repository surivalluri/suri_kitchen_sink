using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyWorkerService.HostedService
{
    public class QueuedService : BackgroundService
    {
        private readonly ILogger<QueuedService> _logger;

        public QueuedService(ITaskQueue taskQueue, ILogger<QueuedService> logger)
        {
            TaskQueue = taskQueue;
            _logger = logger;
        }

        public ITaskQueue TaskQueue { get; }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
            $"Queued Hosted Service is running.{Environment.NewLine}" +
            $"{Environment.NewLine}Tap W to add a work item to the " +
            $"background queue.{Environment.NewLine}");

            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = TaskQueue.Dequeue();

                try
                {
                    if(workItem != null)
                        await workItem(stoppingToken);

                    //_logger.LogInformation($"Finished Processing");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                            $"Error occurred executing {workItem}.", nameof(workItem));
                }

                await Task.Delay(500);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queued Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
