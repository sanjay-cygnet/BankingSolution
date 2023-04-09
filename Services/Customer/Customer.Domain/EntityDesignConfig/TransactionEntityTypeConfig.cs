using Customer.Domain.Entities;
namespace Customer.Domain.EntityDesignConfig;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TransactionEntityTypeConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.TransactionNumber).HasMaxLength(100);

        builder.HasOne(p => p.Account)
            .WithMany(m => m.Transactions)
            .HasForeignKey(f => f.AccountId);

        builder.Property(p => p.TransactionType).HasComment("1=Debit, 2=Credit");//Need sql constraint for this
        builder.Property(p => p.Status).HasDefaultValue(1).HasComment("/1-Pending, 2-Success, 3-Hold,4-Failed");//Need sql constraint for this
    }
}
