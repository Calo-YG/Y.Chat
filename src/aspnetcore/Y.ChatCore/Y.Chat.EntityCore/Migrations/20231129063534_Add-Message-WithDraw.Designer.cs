﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Y.Chat.EntityCore;

#nullable disable

namespace Y.Chat.EntityCore.Migrations
{
    [DbContext(typeof(YChatContext))]
    [Migration("20231129063534_Add-Message-WithDraw")]
    partial class AddMessageWithDraw
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Y.Chat.EntityCore.Domain.ChatDomain.Entities.ChatList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatart")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ChatType")
                        .HasColumnType("int");

                    b.Property<Guid>("ConversationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("FriendId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("LastMessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnReadCount")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("ChatLists");
                });

            modelBuilder.Entity("Y.Chat.EntityCore.Domain.ChatDomain.Entities.Conversation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("GroupNumber")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("ChatGroups");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5bf589f0-91d2-8bca-285a-3a0f2bf71767"),
                            Avatar = "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4",
                            CreationTime = new DateTime(2023, 11, 29, 6, 35, 34, 119, DateTimeKind.Utc).AddTicks(2543),
                            Creator = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "世界频道欢迎来访",
                            GroupNumber = "3164522207",
                            ModificationTime = new DateTime(2023, 11, 29, 6, 35, 34, 119, DateTimeKind.Utc).AddTicks(2544),
                            Modifier = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "世界频道"
                        });
                });

            modelBuilder.Entity("Y.Chat.EntityCore.Domain.ChatDomain.Entities.GroupUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Grouper")
                        .HasColumnType("bit");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "GroupId");

                    b.ToTable("GroupUsers");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("a31329c0-e0b1-6752-ec54-3a0f2bf71767"),
                            GroupId = new Guid("5bf589f0-91d2-8bca-285a-3a0f2bf71767"),
                            CreationTime = new DateTime(2023, 11, 29, 14, 35, 34, 119, DateTimeKind.Local).AddTicks(2568),
                            Creator = new Guid("00000000-0000-0000-0000-000000000000"),
                            Grouper = false,
                            Id = new Guid("7e198562-6d35-94b4-848e-3a0f2bf71767"),
                            IsAdmin = false,
                            ModificationTime = new DateTime(2023, 11, 29, 6, 35, 34, 119, DateTimeKind.Utc).AddTicks(2558),
                            Modifier = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            UserId = new Guid("9387fcb3-409c-1f45-3b71-3a0f2bf71767"),
                            GroupId = new Guid("5bf589f0-91d2-8bca-285a-3a0f2bf71767"),
                            CreationTime = new DateTime(2023, 11, 29, 14, 35, 34, 119, DateTimeKind.Local).AddTicks(2590),
                            Creator = new Guid("00000000-0000-0000-0000-000000000000"),
                            Grouper = false,
                            Id = new Guid("a872a0c8-8657-52b4-31b4-3a0f2bf71767"),
                            IsAdmin = false,
                            ModificationTime = new DateTime(2023, 11, 29, 6, 35, 34, 119, DateTimeKind.Utc).AddTicks(2588),
                            Modifier = new Guid("00000000-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("Y.Chat.EntityCore.Domain.ChatDomain.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MessageType")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Withdraw")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("Y.Chat.EntityCore.Domain.ChatDomain.Entities.Notice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Agred")
                        .HasColumnType("bit");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InviteUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NoticeType")
                        .HasColumnType("int");

                    b.Property<bool>("Read")
                        .HasColumnType("bit");

                    b.Property<Guid>("RecivedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notices");
                });

            modelBuilder.Entity("Y.Chat.EntityCore.Domain.ChatDomain.Entities.SystemMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NotfiyType")
                        .HasColumnType("int");

                    b.Property<Guid?>("RecivedUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("SystemMessages");
                });

            modelBuilder.Entity("Y.Chat.EntityCore.Domain.FileDomain.Entitis.FileSystem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("FileType")
                        .HasColumnType("int");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Isfolder")
                        .HasColumnType("bit");

                    b.Property<string>("MinioName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("FileSystems");
                });

            modelBuilder.Entity("Y.Chat.EntityCore.Domain.UserDomain.Entities.EmailRecords", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Conteent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiredTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RecordType")
                        .HasColumnType("int");

                    b.Property<DateTime>("SendTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("EmailRecords");
                });

            modelBuilder.Entity("Y.Chat.EntityCore.Domain.UserDomain.Entities.Friends", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FriendId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Modifier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("Y.Chat.EntityCore.Domain.UserDomain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Autograph")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasDefaultValue("https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4");

                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("LoginType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Account")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a31329c0-e0b1-6752-ec54-3a0f2bf71767"),
                            Account = "3164522206",
                            Avatar = "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4",
                            Email = "3164522206@qq.com",
                            LoginType = 0,
                            Name = "admin",
                            Password = "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4="
                        },
                        new
                        {
                            Id = new Guid("9387fcb3-409c-1f45-3b71-3a0f2bf71767"),
                            Account = "3164522207",
                            Avatar = "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4",
                            Email = "3164522206@qq.com",
                            LoginType = 0,
                            Name = "wyg",
                            Password = "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4="
                        });
                });

            modelBuilder.Entity("Y.Chat.EntityCore.Domain.FileDomain.Entitis.FileSystem", b =>
                {
                    b.HasOne("Y.Chat.EntityCore.Domain.ChatDomain.Entities.Conversation", "ChatGroup")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.Navigation("ChatGroup");
                });
#pragma warning restore 612, 618
        }
    }
}