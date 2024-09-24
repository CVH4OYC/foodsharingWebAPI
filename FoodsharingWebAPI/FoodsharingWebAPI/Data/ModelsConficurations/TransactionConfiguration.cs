using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FoodsharingWebAPI.Models;

namespace FoodsharingWebAPI.Data.ModelsConficurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        /// <summary>
        /// Метод конфигурации модели Transaction
        /// </summary>
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
