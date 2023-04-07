using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Domain.EntityDesignConfig
{
    public class CustomerEntityTypeConfig : IEntityTypeConfiguration<Entities.Customer>
    {
        public void Configure(EntityTypeBuilder<Entities.Customer> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.FirstName).HasMaxLength(100);
            builder.Property(p => p.LastName).HasMaxLength(100);
            builder.Property(p => p.Address).HasMaxLength(255);
            builder.Property(p => p.City).HasMaxLength(255);
            builder.Property(p => p.Mobile).HasMaxLength(20);
            builder.Property(p => p.Email).HasMaxLength(255);
            builder.Property(p => p.CreatedById).HasMaxLength(50);
            builder.Property(p => p.LastModifiedById).HasMaxLength(50);
        }
    }
}