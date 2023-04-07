using BuildingBlocks.EventBus.QueuePublisher;
using BuildingBlocks.EventBus.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace BuildingBlocks.EventBus.Extensions
{
    public static class ServiceExtensions
    {
        #region Register EventBus
        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration, string eventSource = "")
        {
            var eventBusConnection = configuration["RabbitMQ:EventBusConnection"];
            var eventBusUserName = configuration["RabbitMQ:EventBusUserName"];
            var eventBusPassword = configuration["RabbitMQ:EventBusPassword"];
            var eventBusRetryCount = configuration["RabbitMQ:EventBusRetryCount"];

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = eventBusConnection,
                    //Port = 15672,
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(eventBusUserName))
                {
                    factory.UserName = eventBusUserName;
                }
                else
                    logger.LogWarning($"{nameof(AddRabbitMq)} => no username found for event bus config");

                if (!string.IsNullOrEmpty(eventBusPassword))
                {
                    factory.Password = eventBusPassword;
                }
                logger.LogWarning($"{nameof(AddRabbitMq)} => no password found for event bus config");

                var retryCount = 3;//default value
                if (!string.IsNullOrEmpty(eventBusRetryCount))
                {
                    retryCount = int.Parse(eventBusRetryCount);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            //Register queue dependency
            services.AddTransient<IDefaultQueuePublisher, DefaultQueuePublisher>();
            services.AddTransient<IEmailQueuePublisher, EmailQueuePublisher>();
        }


        #endregion
    }
}
