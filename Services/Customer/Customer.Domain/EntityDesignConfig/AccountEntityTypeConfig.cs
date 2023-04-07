using Customer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Domain.EntityDesignConfig
{
    public class AccountEntityTypeConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.AccountNumber).HasMaxLength(50);
            //Foreign Keys
            builder.HasOne(h => h.BankBranch)
                .WithMany(m => m.Accounts)
                .HasForeignKey(f => f.BankBranchId);

            builder.HasOne(h => h.Customer)
              .WithMany(m => m.Accounts)
              .HasForeignKey(f => f.CustomerId);

            builder.Property(p => p.Type).HasComment("1=Saving, 2=Current");//Need sql constraint for this

            builder.Property(p => p.Status).HasComment("1=Active, 2=Block, 3=Suspended");//Need sql constraint for this
            builder.Property(p => p.CreatedById).HasMaxLength(50).IsRequired(false);
            builder.Property(p => p.LastModifiedById).HasMaxLength(50);
        }
    }
}