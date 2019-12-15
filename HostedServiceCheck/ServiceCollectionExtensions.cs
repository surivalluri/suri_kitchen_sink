using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWorkerService.HostedService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDocHandling(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //var configuration = services.FirstOrDefault(_ => _.ServiceType == typeof(IConfigurationRoot)).ImplementationInstance as IConfigurationRoot;

            //var configuration = GetAllImplementationsOf<IConfigurationRoot>().FirstOrDefault() as IConfigurationRoot;

            //if (configuration == null)
            //    throw new Exception($"No Implementations of {configuration}");

            //services.Configure<FirstSubscriptionSettings>(configuration.GetSection("FirstSubscriptionSettings"));

            //services.AddTransient<IHostedService,QueuedService>();

            //services.AddHttpClient<IHostedService, FirstSubscription>();

            services.AddTransient<IHostedService, FirstSubscription>();

            services.AddTransient(typeof(IHostedService), typeof(QueuedService));

            services.AddSingleton<ITaskQueue, TaskQueue>();

            services.AddSingleton<IMsgProcessorResolver, MsgProcessorResolver>();
            services.AddSingleton<INPService, NPService>();

            services.AddLogging();

            //GetAllImplementationsOf<IMsgProcessor>().ToList().ForEach(t => services.AddSingleton(t));

            //services.AddSingleton<IMsgProcessor, NEMsgProcessor>

            return services;
        }

        public static IEnumerable<Type> GetAllImplementationsOf<TIn>()
        {
            var type = typeof(TIn);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            return types;
        }
    }
}
