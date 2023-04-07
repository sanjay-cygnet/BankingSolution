using Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Domain.EntityDesignConfig
{
    public class BankBranchEntityTypeConfig : IEntityTypeConfiguration<BankBranch>
    {
        public void Configure(EntityTypeBuilder<BankBranch> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Code).IsRequired().HasMaxLength(20);
            builder.Property(p => p.City).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Currency).IsRequired().HasDefaultValue(1);
        }
    }
}