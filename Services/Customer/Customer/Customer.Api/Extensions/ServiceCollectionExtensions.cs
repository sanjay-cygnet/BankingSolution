namespace Customer.Api.Extensions;
#nullable disable
using BuildingBlocks.EventBus.Extensions;
using BuildingBlocks.Repository.Extensions;
using BuildingBlocks.Shared.Constants;
using BuildingBlocks.Shared.Extensions;
using BuildingBlocks.Subscriptions.Infrastructure;
using Customer.Infrastructure.Context;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shared.Constants;
using Shared.PipelineBehaviors;
using System.Reflection;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(op =>
        {
            op.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Customer API",
                Version = "v1",
            });
        });

        services.AddScoped<DomainEventDispatcher>();

        string connectionString = configuration["DatabaseConnection"];

        if (!connectionString.IsNull())
        {
            services.AddDbContext<CustomerDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(serviceProvider.GetService<DomainEventDispatcher>());
            }).AddUnitOfWork<CustomerDbContext>();
        }
        else
            throw new InvalidOperationException(CustomerServiceConstants.Messages.NoConnectionStringProvided);

        services.AddAutoMapper(Assembly.Load(CustomerServiceAssemblies.CustomerApplication));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load(CustomerServiceAssemblies.CustomerApplication)));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.Load(CustomerServiceAssemblies.CustomerApplication));

        services.AddRabbitMq(configuration);
    }
}
