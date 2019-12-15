using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyWorkerService.HostedService
{
    public class FirstSubscription : BackgroundService
    {
        private readonly ITaskQueue _taskQueue;
        private readonly ILogger<FirstSubscription> _logger;
        private readonly HttpClient _httpClient;
        private readonly INPService _nPService;
        private readonly FirstSubscriptionSettings _firstSubscriptionSettings;

        public FirstSubscription(ITaskQueue taskQueue, 
            ILogger<FirstSubscription> logger, 
            IOptions<FirstSubscriptionSettings> firstSubscriptionSettings
            /*HttpClient httpClient*/,
            INPService nPService)
        {

            _taskQueue = taskQueue;
            _logger = logger;
            _httpClient = new HttpClient();
            _nPService = nPService;
            _firstSubscriptionSettings = firstSubscriptionSettings.Value ?? throw new ArgumentNullException(nameof(firstSubscriptionSettings));
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"FirstSubscription Hosted Service is running.");

                await BackgroundProcessing(stoppingToken);
            }
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var items = await _httpClient.GetAsync(_firstSubscriptionSettings.RUrl, stoppingToken);

                var deItems = JsonConvert.DeserializeObject<List<Document>>(await items.Content.ReadAsStringAsync());

                foreach (var item in deItems)
                {
                    _taskQueue.Queue(stopping => { return HandleWorkItem(item, stoppingToken); }, stoppingToken);
                }
            }
        }

        protected async Task HandleWorkItem(Document docs, CancellationToken cancellationToken)
        {
            if (docs == null) throw new ArgumentNullException(nameof(docs));

            _ = await _nPService.Process(docs, cancellationToken);

            _logger.LogInformation($"Finished Processing of {docs.Name}");

        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("FirstSubscription Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }

    public class FirstSubscriptionSettings
    {
        public string RUrl { get; set; }

        public string CUrl { get; set; }
    }
}
