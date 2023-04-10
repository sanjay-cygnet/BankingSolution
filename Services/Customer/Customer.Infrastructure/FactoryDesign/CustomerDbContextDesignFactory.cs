namespace Customer.Infrastructure.FactoryDesign;

using Customer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

/// <summary>
/// CustomerDbContextDesignFactory : just used for migration purpose only
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory&lt;Customer.Infrastructure.Context.CustomerDbContext&gt;" />
public class CustomerDbContextDesignFactory : IDesignTimeDbContextFactory<CustomerDbContext>
{
    public CustomerDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
        var connectionString = configuration["DatabaseConnection"];

        var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new CustomerDbContext(optionsBuilder.Options);
    }
}
