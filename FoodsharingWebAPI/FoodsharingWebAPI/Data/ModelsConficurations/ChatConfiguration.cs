using FoodsharingWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FoodsharingWebAPI.Data.ModelsConficurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        /// <summary>
        /// Метод конфигурации модели Chat
        /// </summary>
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasOne(c => c.FirstUser)
            .WithMany(u => u.Chats)
            .HasForeignKey(c => c.FirstUserId)
            .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.SecondUser)
            .WithMany()
            .HasForeignKey(c => c.SecondUserId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
