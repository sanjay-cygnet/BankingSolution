using BuildingBlocks.Shared.Constants;
using Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Customer.Infrastructure.Context
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<BankBranch> BankBranch { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Domain.Entities.Customer> Customers { get; set; }

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
                if (string.IsNullOrEmpty(connectionString))
                    optionsBuilder.UseSqlServer(connectionString);
            }
            //optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }
    }
}
