using FoodsharingWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FoodsharingWebAPI.ModelsConficurations
{
    public class ChatConfiguration:IEntityTypeConfiguration<Chat>
    {
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
