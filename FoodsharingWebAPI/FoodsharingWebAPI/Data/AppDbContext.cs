using FoodsharingWebAPI.Models;
using FoodsharingWebAPI.Data.ModelsConficurations;
using Microsoft.EntityFrameworkCore;

namespace FoodsharingWebAPI.Data
{
    /// <summary>
    /// Контекст БД
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        /// <summary>
        /// Коллекция сущностей типа Address
        /// </summary>
        public DbSet<Address> Addresses { get; set; }
        /// <summary>
        /// Коллекция сущностей типа Announcement
        /// </summary>
        public DbSet<Announcement> Announcements { get; set; }
        /// <summary>
        /// Коллекция сущностей типа Category
        /// </summary>
        public DbSet<Category> Categories { get; set; }
        /// <summary>
        /// Коллекция сущностей типа Chat
        /// </summary>
        public DbSet<Chat> Chats { get; set; }
        /// <summary>
        /// Коллекция сущностей типа Message
        /// </summary>
        public DbSet<Message> Messages { get; set; }
        /// <summary>
        /// Коллекция сущностей типа MessageStatus
        /// </summary>
        public DbSet<MessageStatus> MessageStatuses { get; set; }
        /// <summary>
        /// Коллекция сущностей типа Organization
        /// </summary>
        public DbSet<Organization> Organizations { get; set; }
        /// <summary>
        /// Коллекция сущностей типа Profile
        /// </summary>
        public DbSet<Profile> Profiles { get; set; }
        /// <summary>
        /// Коллекция сущностей типа Representative
        /// </summary>
        public DbSet<Representative> Representatives { get; set; }
        /// <summary>
        /// Коллекция сущностей типа Role
        /// </summary>
        public DbSet<Role> Roles { get; set; }
        /// <summary>
        /// Коллекция сущностей типа User
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Коллекция сущностей типа Transaction
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }
        /// <summary>
        /// Коллекция сущностей типа TransactionStatus
        /// </summary>
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        /// <summary>
        /// Коллекция сущностей типа UserRole
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            SeedData.Seed(modelBuilder);
        }
    }
}
