using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Y.Chat.EntityCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChatList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChatGroups",
                keyColumn: "Id",
                keyValue: new Guid("6e71ca99-6d81-8c18-f6ca-3a0ef5d3e188"));

            migrationBuilder.DeleteData(
                table: "GroupUsers",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { new Guid("6e71ca99-6d81-8c18-f6ca-3a0ef5d3e188"), new Guid("87b4c3ab-5eaf-703e-e2b6-3a0ef5d3e188") });

            migrationBuilder.DeleteData(
                table: "GroupUsers",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { new Guid("6e71ca99-6d81-8c18-f6ca-3a0ef5d3e188"), new Guid("e47236af-a5a5-6d2e-3e71-3a0ef5d3e188") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("87b4c3ab-5eaf-703e-e2b6-3a0ef5d3e188"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e47236af-a5a5-6d2e-3e71-3a0ef5d3e188"));

            migrationBuilder.DropColumn(
                name: "LastMessageId",
                table: "ChatLists");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatId",
                table: "ChatLists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "ChatGroups",
                columns: new[] { "Id", "Avatar", "CreationTime", "Creator", "Description", "GroupNumber", "ModificationTime", "Modifier", "Name", "UserId" },
                values: new object[] { new Guid("02cdecd2-8e7c-0d2b-ab65-3a0eff3d84d3"), "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4", new DateTime(2023, 11, 20, 14, 9, 34, 931, DateTimeKind.Utc).AddTicks(826), new Guid("00000000-0000-0000-0000-000000000000"), "世界频道欢迎来访", "3164522207", new DateTime(2023, 11, 20, 14, 9, 34, 931, DateTimeKind.Utc).AddTicks(828), new Guid("00000000-0000-0000-0000-000000000000"), "世界频道", null });

            migrationBuilder.InsertData(
                table: "GroupUsers",
                columns: new[] { "GroupId", "UserId", "CreationTime", "Creator", "Grouper", "Id", "IsAdmin", "ModificationTime", "Modifier", "NickName" },
                values: new object[,]
                {
                    { new Guid("02cdecd2-8e7c-0d2b-ab65-3a0eff3d84d3"), new Guid("082df594-124d-084e-1965-3a0eff3d84d3"), new DateTime(2023, 11, 20, 22, 9, 34, 931, DateTimeKind.Local).AddTicks(896), new Guid("00000000-0000-0000-0000-000000000000"), false, new Guid("f7c91f4c-8d9f-4fd1-d334-3a0eff3d84d3"), false, new DateTime(2023, 11, 20, 14, 9, 34, 931, DateTimeKind.Utc).AddTicks(881), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("02cdecd2-8e7c-0d2b-ab65-3a0eff3d84d3"), new Guid("bb6250ae-e0d7-365f-49af-3a0eff3d84d3"), new DateTime(2023, 11, 20, 22, 9, 34, 931, DateTimeKind.Local).AddTicks(853), new Guid("00000000-0000-0000-0000-000000000000"), false, new Guid("f2c5ac06-2f58-864f-e756-3a0eff3d84d3"), false, new DateTime(2023, 11, 20, 14, 9, 34, 931, DateTimeKind.Utc).AddTicks(842), new Guid("00000000-0000-0000-0000-000000000000"), null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Account", "Autograph", "Avatar", "Email", "LoginType", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("082df594-124d-084e-1965-3a0eff3d84d3"), "3164522207", null, "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4", "3164522206@qq.com", 0, "wyg", "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4=" },
                    { new Guid("bb6250ae-e0d7-365f-49af-3a0eff3d84d3"), "3164522206", null, "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4", "3164522206@qq.com", 0, "admin", "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4=" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChatGroups",
                keyColumn: "Id",
                keyValue: new Guid("02cdecd2-8e7c-0d2b-ab65-3a0eff3d84d3"));

            migrationBuilder.DeleteData(
                table: "GroupUsers",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { new Guid("02cdecd2-8e7c-0d2b-ab65-3a0eff3d84d3"), new Guid("082df594-124d-084e-1965-3a0eff3d84d3") });

            migrationBuilder.DeleteData(
                table: "GroupUsers",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { new Guid("02cdecd2-8e7c-0d2b-ab65-3a0eff3d84d3"), new Guid("bb6250ae-e0d7-365f-49af-3a0eff3d84d3") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("082df594-124d-084e-1965-3a0eff3d84d3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bb6250ae-e0d7-365f-49af-3a0eff3d84d3"));

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "ChatLists");

            migrationBuilder.AddColumn<Guid>(
                name: "LastMessageId",
                table: "ChatLists",
                type: "uniqueidentifier",
                nullable: true);

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
        }
    }
}
