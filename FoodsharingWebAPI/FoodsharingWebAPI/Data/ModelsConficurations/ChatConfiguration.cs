using FoodsharingWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FoodsharingWebAPI.Data.ModelsConficurations
{
    /// <summary>
    /// Конфигурация модели Chat
    /// </summary>
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        /// <summary>
        /// Конфигурирует модель Chat, задавая связи между сущностями и правила удаления
        /// </summary>
        /// <param name="builder">builder для конфигурации сущности</param>
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
