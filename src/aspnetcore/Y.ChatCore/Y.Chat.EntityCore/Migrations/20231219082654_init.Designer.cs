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
    [Migration("20231219082654_init")]
    partial class init
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
                            Id = new Guid("87dcbed7-e1b5-bff9-bab4-3a0f935c3619"),
                            Avatar = "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4",
                            CreationTime = new DateTime(2023, 12, 19, 8, 26, 54, 361, DateTimeKind.Utc).AddTicks(7148),
                            Creator = new Guid("00000000-0000-0000-0000-000000000000"),
                            Description = "世界频道欢迎来访",
                            GroupNumber = "3164522207",
                            ModificationTime = new DateTime(2023, 12, 19, 8, 26, 54, 361, DateTimeKind.Utc).AddTicks(7149),
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
                            UserId = new Guid("251aa9a0-7fdb-1819-1814-3a0f935c3619"),
                            GroupId = new Guid("87dcbed7-e1b5-bff9-bab4-3a0f935c3619"),
                            CreationTime = new DateTime(2023, 12, 19, 16, 26, 54, 361, DateTimeKind.Local).AddTicks(7169),
                            Creator = new Guid("00000000-0000-0000-0000-000000000000"),
                            Grouper = false,
                            Id = new Guid("6751933e-5cc6-0f91-c2f4-3a0f935c3619"),
                            IsAdmin = false,
                            ModificationTime = new DateTime(2023, 12, 19, 8, 26, 54, 361, DateTimeKind.Utc).AddTicks(7163),
                            Modifier = new Guid("00000000-0000-0000-0000-000000000000")
                        },
                        new
                        {
                            UserId = new Guid("adee9180-084b-38c1-d05c-3a0f935c3619"),
                            GroupId = new Guid("87dcbed7-e1b5-bff9-bab4-3a0f935c3619"),
                            CreationTime = new DateTime(2023, 12, 19, 16, 26, 54, 361, DateTimeKind.Local).AddTicks(7178),
                            Creator = new Guid("00000000-0000-0000-0000-000000000000"),
                            Grouper = false,
                            Id = new Guid("7ea84ad0-7263-63d5-227a-3a0f935c3619"),
                            IsAdmin = false,
                            ModificationTime = new DateTime(2023, 12, 19, 8, 26, 54, 361, DateTimeKind.Utc).AddTicks(7176),
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

            modelBuilder.Entity("Y.Chat.EntityCore.Domain.SystemDomain.RequestLogInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Exception")
                        .HasMaxLength(550)
                        .HasColumnType("nvarchar(550)");

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Param")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RequestUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Success")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("RequestLogInfos");
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
                            Id = new Guid("251aa9a0-7fdb-1819-1814-3a0f935c3619"),
                            Account = "3164522206",
                            Avatar = "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4",
                            Email = "3164522206@qq.com",
                            LoginType = 0,
                            Name = "admin",
                            Password = "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4="
                        },
                        new
                        {
                            Id = new Guid("adee9180-084b-38c1-d05c-3a0f935c3619"),
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
