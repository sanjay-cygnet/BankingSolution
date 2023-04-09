namespace Customer.Api.Extensions;

using BuildingBlocks.EventBus.Extensions;
using Microsoft.OpenApi.Models;
using Notifications.Api.QueueConsumers;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(op =>
        {
            op.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Notification API",
                Version = "v1",
            });
        });
        services.AddHostedService<EmailQueueConsumer>();
        services.AddRabbitMq(configuration);
    }
}
