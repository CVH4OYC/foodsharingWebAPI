using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FoodsharingWebAPI.Models;

namespace FoodsharingWebAPI.ModelsConficurations
{
    public class TransactionConfiguration: IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne(t => t.Sender)
            .WithMany(u => u.Transactions)
            .HasForeignKey(c => c.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Recipient)
            .WithMany()
            .HasForeignKey(c => c.RecipientId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
