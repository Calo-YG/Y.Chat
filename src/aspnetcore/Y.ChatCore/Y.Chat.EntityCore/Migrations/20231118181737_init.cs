using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Y.Chat.EntityCore.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GroupNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnReadCount = table.Column<int>(type: "int", nullable: false),
                    LastMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Avatart = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatType = table.Column<int>(type: "int", nullable: false),
                    FriendId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Conteent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecordType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FriendId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupUsers",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Grouper = table.Column<bool>(type: "bit", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUsers", x => new { x.UserId, x.GroupId });
                });

            migrationBuilder.CreateTable(
                name: "Notices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InviteUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecivedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Agred = table.Column<bool>(type: "bit", nullable: false),
                    NoticeType = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotfiyType = table.Column<int>(type: "int", nullable: false),
                    RecivedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Account = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValue: "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4"),
                    LoginType = table.Column<int>(type: "int", nullable: false),
                    Autograph = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Isfolder = table.Column<bool>(type: "bit", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    MinioName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileSystems_ChatGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ChatGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ChatGroups",
                columns: new[] { "Id", "Avatar", "CreationTime", "Creator", "Description", "GroupNumber", "ModificationTime", "Modifier", "Name", "UserId" },
                values: new object[] { new Guid("6e71ca99-6d81-8c18-f6ca-3a0ef5d3e188"), "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4", new DateTime(2023, 11, 18, 18, 17, 36, 904, DateTimeKind.Utc).AddTicks(4906), new Guid("00000000-0000-0000-0000-000000000000"), "世界频道欢迎来访", "3164522207", new DateTime(2023, 11, 18, 18, 17, 36, 904, DateTimeKind.Utc).AddTicks(4908), new Guid("00000000-0000-0000-0000-000000000000"), "世界频道", null });

            migrationBuilder.InsertData(
                table: "GroupUsers",
                columns: new[] { "GroupId", "UserId", "CreationTime", "Creator", "Grouper", "Id", "IsAdmin", "ModificationTime", "Modifier", "NickName" },
                values: new object[,]
                {
                    { new Guid("6e71ca99-6d81-8c18-f6ca-3a0ef5d3e188"), new Guid("87b4c3ab-5eaf-703e-e2b6-3a0ef5d3e188"), new DateTime(2023, 11, 19, 2, 17, 36, 904, DateTimeKind.Local).AddTicks(4962), new Guid("00000000-0000-0000-0000-000000000000"), false, new Guid("56ee8498-43a8-707d-a924-3a0ef5d3e188"), false, new DateTime(2023, 11, 18, 18, 17, 36, 904, DateTimeKind.Utc).AddTicks(4959), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("6e71ca99-6d81-8c18-f6ca-3a0ef5d3e188"), new Guid("e47236af-a5a5-6d2e-3e71-3a0ef5d3e188"), new DateTime(2023, 11, 19, 2, 17, 36, 904, DateTimeKind.Local).AddTicks(4935), new Guid("00000000-0000-0000-0000-000000000000"), false, new Guid("ba988693-193f-ba6a-fecd-3a0ef5d3e188"), false, new DateTime(2023, 11, 18, 18, 17, 36, 904, DateTimeKind.Utc).AddTicks(4923), new Guid("00000000-0000-0000-0000-000000000000"), null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Account", "Autograph", "Avatar", "Email", "LoginType", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("87b4c3ab-5eaf-703e-e2b6-3a0ef5d3e188"), "3164522207", null, "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4", "3164522206@qq.com", 0, "wyg", "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4=" },
                    { new Guid("e47236af-a5a5-6d2e-3e71-3a0ef5d3e188"), "3164522206", null, "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4", "3164522206@qq.com", 0, "admin", "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4=" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatLists_Id",
                table: "ChatLists",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_Id",
                table: "ChatMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FileSystems_GroupId",
                table: "FileSystems",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Account",
                table: "Users",
                column: "Account",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatLists");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "EmailRecords");

            migrationBuilder.DropTable(
                name: "FileSystems");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "GroupUsers");

            migrationBuilder.DropTable(
                name: "Notices");

            migrationBuilder.DropTable(
                name: "SystemMessages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ChatGroups");
        }
    }
}
