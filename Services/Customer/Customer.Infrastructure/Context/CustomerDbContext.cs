namespace Customer.Infrastructure.Context;

using BuildingBlocks.Shared.Constants;
using BuildingBlocks.Shared.Extensions;
using Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

public class CustomerDbContext : DbContext
{
    #region Ctor
    public CustomerDbContext(DbContextOptions<CustomerDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    #endregion

    #region DbSet
    public DbSet<BankBranch> BankBranch { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Domain.Entities.Customer> Customers { get; set; }
    #endregion

    #region Method(s)
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.Load(CustomerServiceAssemblies.CustomerDomain));
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var connectionString = configuration["DatabaseConnection"];
            if (!connectionString.IsNull())
                optionsBuilder.UseSqlServer(connectionString);
        }
        //optionsBuilder.EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }
    #endregion
}
