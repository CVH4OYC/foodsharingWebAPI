using FoodsharingWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodsharingWebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageStatus> MessageStatuses { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Representative> Representatives { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // нужно явно прописывать связь, если два внешних ключа на одну и ту же сущность
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.FirstUser)
                .WithMany(u => u.Chats)
                .HasForeignKey(c => c.FirstUserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.SecondUser)
                .WithMany()
                .HasForeignKey(c => c.SecondUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Sender)
                .WithMany(u => u.Transactions)
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Recipient)
                .WithMany()
                .HasForeignKey(c => c.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "FirstUser",
                    Password = "1234"
                },
                new User
                {
                    Id = 2,
                    UserName = "SecondUser",
                    Password = "4321"
                });
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "ADMIN"
                },
                new Role
                {
                    Id = 2,
                    Name = "USER"
                });
            modelBuilder.Entity<Profile>().HasData(
                new Profile
                {
                    Id = 1,
                    FirstName = "Василий",
                    LastName = "Пупкин",
                    Image = "https://upload.wikimedia.org/wikipedia/commons/0/0e/Felis_silvestris_silvestris.jpg",
                    Bio = "Интереснейшее описание",
                    UserId = 1
                },
                new Profile
                {
                    Id = 2,
                    FirstName = "Александр",
                    UserId = 2
                });
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = 1,
                    UserId = 1,
                    RoleId = 1
                },
                new UserRole
                {
                    Id = 2,
                    UserId = 2,
                    RoleId = 2
                });
            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = 1,
                    Region = "Пермский край",
                    City = "Пермь",
                    Street = "Улица",
                    House = "2А"
                });
            modelBuilder.Entity<Organization>().HasData(
                new Organization
                {
                    Id = 1,
                    Name = "Магазин",
                    AddressId = 1,
                    Phone = "88005553535",
                    Email = "test@mail.com",
                    Website = "shop.com",
                    Description = "Супер информативное описание"
                });
            modelBuilder.Entity<Representative>().HasData(
                new Representative
                {
                    Id = 1,
                    UserId = 2,
                    OrganizationId = 1
                });
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Консервы"
                },
                new Category
                {
                    Id = 2,
                    Name = "Домашняя"
                },
                new Category
                {
                    Id = 3,
                    Name = "Сладкое"
                },
                new Category
                {
                    Id = 4,
                    Name = "Крупа"
                },
                new Category
                {
                    Id = 5,
                    Name = "Снеки"
                });
            modelBuilder.Entity<Announcement>().HasData(
                new Announcement
                {
                    Id = 1,
                    Title = "Гречка",
                    Description = "Не нужна гречка, заберите пожалуйста!",
                    AddressId = 1,
                    DateCreation = DateTime.Now.ToUniversalTime(), // нужно преобразовывать в универсальный формат
                    ExpirationDate = new DateTime(2025, 4, 15).ToUniversalTime(),
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRHknlM8LS4l-x7kEYfmVZttH2PLnPW-EUKUw&s",
                    CategoryId = 4,
                    UserId = 2
                });
            modelBuilder.Entity<TransactionStatus>().HasData(
                new TransactionStatus
                {
                    Id = 1,
                    Name = "Завершена"
                },
                new TransactionStatus
                {
                    Id = 2,
                    Name = "В процессе"
                },
                new TransactionStatus
                {
                    Id = 3,
                    Name = "Отменена"
                });
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = 1,
                    SenderId = 2,
                    RecipientId = 1,
                    TransactionDate = DateTime.Now.ToUniversalTime(),
                    StatusId = 2,
                    AnnouncementId = 1
                });
            modelBuilder.Entity<Chat>().HasData(
                new Chat
                {
                    Id = 1,
                    FirstUserId = 1,
                    SecondUserId = 2
                });
            modelBuilder.Entity<MessageStatus>().HasData(
                new MessageStatus
                {
                    Id = 1,
                    Name = "Отправлено"
                },
                new MessageStatus
                {
                    Id = 2,
                    Name = "Не отправлено"
                },
                new MessageStatus
                {
                    Id = 3,
                    Name = "Прочитано"
                });
            modelBuilder.Entity<Message>().HasData(
                new Message
                {
                    Id = 1,
                    SenderId = 2,
                    ChatId = 1,
                    Text = "Добрый день, когда и как можно забрать гречку?",
                    Date = DateTime.Now.ToUniversalTime(),
                    StatusId = 1,
                });
        }
    }
}
