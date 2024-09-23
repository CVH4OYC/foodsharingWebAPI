﻿// <auto-generated />
using System;
using FoodsharingWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodsharingWebAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FoodsharingWebAPI.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("House")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Пермь",
                            House = "2А",
                            Region = "Пермский край",
                            Street = "Улица"
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Announcements");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressId = 1,
                            CategoryId = 4,
                            DateCreation = new DateTime(2024, 9, 23, 10, 23, 31, 899, DateTimeKind.Utc).AddTicks(8086),
                            Description = "Не нужна гречка, заберите пожалуйста!",
                            ExpirationDate = new DateTime(2025, 4, 14, 19, 0, 0, 0, DateTimeKind.Utc),
                            Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRHknlM8LS4l-x7kEYfmVZttH2PLnPW-EUKUw&s",
                            Title = "Гречка",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Консервы"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Домашняя"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Сладкое"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Крупа"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Снеки"
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FirstUserId")
                        .HasColumnType("integer");

                    b.Property<int>("SecondUserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FirstUserId");

                    b.HasIndex("SecondUserId");

                    b.ToTable("Chats");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstUserId = 1,
                            SecondUserId = 2
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("File")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("SenderId");

                    b.HasIndex("StatusId");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChatId = 1,
                            Date = new DateTime(2024, 9, 23, 10, 23, 31, 899, DateTimeKind.Utc).AddTicks(8454),
                            SenderId = 2,
                            StatusId = 1,
                            Text = "Добрый день, когда и как можно забрать гречку?"
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.MessageStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MessageStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Отправлено"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Не отправлено"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Прочитано"
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Website")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Organizations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressId = 1,
                            Description = "Супер информативное описание",
                            Email = "test@mail.com",
                            Name = "Магазин",
                            Phone = "88005553535",
                            Website = "shop.com"
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Bio = "Интереснейшее описание",
                            FirstName = "Василий",
                            Image = "https://upload.wikimedia.org/wikipedia/commons/0/0e/Felis_silvestris_silvestris.jpg",
                            LastName = "Пупкин",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Александр",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Representative", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("OrganizationId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Representatives");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrganizationId = 1,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            Name = "USER"
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnnouncementId")
                        .HasColumnType("integer");

                    b.Property<int>("RecipientId")
                        .HasColumnType("integer");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AnnouncementId");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.HasIndex("StatusId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AnnouncementId = 1,
                            RecipientId = 1,
                            SenderId = 2,
                            StatusId = 2,
                            TransactionDate = new DateTime(2024, 9, 23, 10, 23, 31, 899, DateTimeKind.Utc).AddTicks(8287)
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.TransactionStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TransactionStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Завершена"
                        },
                        new
                        {
                            Id = 2,
                            Name = "В процессе"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Отменена"
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "1234",
                            UserName = "FirstUser"
                        },
                        new
                        {
                            Id = 2,
                            Password = "4321",
                            UserName = "SecondUser"
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            RoleId = 2,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Announcement", b =>
                {
                    b.HasOne("FoodsharingWebAPI.Models.Address", "Address")
                        .WithMany("Announcements")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodsharingWebAPI.Models.Category", "Category")
                        .WithMany("Announcements")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodsharingWebAPI.Models.User", "User")
                        .WithMany("Announcements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Chat", b =>
                {
                    b.HasOne("FoodsharingWebAPI.Models.User", "FirstUser")
                        .WithMany("Chats")
                        .HasForeignKey("FirstUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FoodsharingWebAPI.Models.User", "SecondUser")
                        .WithMany()
                        .HasForeignKey("SecondUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FirstUser");

                    b.Navigation("SecondUser");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Message", b =>
                {
                    b.HasOne("FoodsharingWebAPI.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodsharingWebAPI.Models.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodsharingWebAPI.Models.MessageStatus", "Status")
                        .WithMany("Messages")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Sender");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Organization", b =>
                {
                    b.HasOne("FoodsharingWebAPI.Models.Address", "Address")
                        .WithOne("Organization")
                        .HasForeignKey("FoodsharingWebAPI.Models.Organization", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Profile", b =>
                {
                    b.HasOne("FoodsharingWebAPI.Models.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("FoodsharingWebAPI.Models.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Representative", b =>
                {
                    b.HasOne("FoodsharingWebAPI.Models.Organization", "Organization")
                        .WithMany("Representatives")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodsharingWebAPI.Models.User", "User")
                        .WithOne("Representative")
                        .HasForeignKey("FoodsharingWebAPI.Models.Representative", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Transaction", b =>
                {
                    b.HasOne("FoodsharingWebAPI.Models.Announcement", "Announcement")
                        .WithMany("Transactions")
                        .HasForeignKey("AnnouncementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodsharingWebAPI.Models.User", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FoodsharingWebAPI.Models.User", "Sender")
                        .WithMany("Transactions")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FoodsharingWebAPI.Models.TransactionStatus", "Status")
                        .WithMany("Transactions")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Announcement");

                    b.Navigation("Recipient");

                    b.Navigation("Sender");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.UserRole", b =>
                {
                    b.HasOne("FoodsharingWebAPI.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodsharingWebAPI.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Address", b =>
                {
                    b.Navigation("Announcements");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Announcement", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Category", b =>
                {
                    b.Navigation("Announcements");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.MessageStatus", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Organization", b =>
                {
                    b.Navigation("Representatives");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.TransactionStatus", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("FoodsharingWebAPI.Models.User", b =>
                {
                    b.Navigation("Announcements");

                    b.Navigation("Chats");

                    b.Navigation("Messages");

                    b.Navigation("Profile");

                    b.Navigation("Representative");

                    b.Navigation("Transactions");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
