using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Y.Chat.EntityCore.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageWithDraw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChatGroups",
                keyColumn: "Id",
                keyValue: new Guid("9a46221e-ee1a-e580-f079-3a0effb21773"));

            migrationBuilder.DeleteData(
                table: "GroupUsers",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { new Guid("9a46221e-ee1a-e580-f079-3a0effb21773"), new Guid("cabed7f6-055a-6e31-5f86-3a0effb21773") });

            migrationBuilder.DeleteData(
                table: "GroupUsers",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { new Guid("9a46221e-ee1a-e580-f079-3a0effb21773"), new Guid("dba9a606-c418-6396-8c8e-3a0effb21773") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cabed7f6-055a-6e31-5f86-3a0effb21773"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dba9a606-c418-6396-8c8e-3a0effb21773"));

            migrationBuilder.AddColumn<bool>(
                name: "Withdraw",
                table: "ChatMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "ChatGroups",
                columns: new[] { "Id", "Avatar", "CreationTime", "Creator", "Description", "GroupNumber", "ModificationTime", "Modifier", "Name", "UserId" },
                values: new object[] { new Guid("5bf589f0-91d2-8bca-285a-3a0f2bf71767"), "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4", new DateTime(2023, 11, 29, 6, 35, 34, 119, DateTimeKind.Utc).AddTicks(2543), new Guid("00000000-0000-0000-0000-000000000000"), "世界频道欢迎来访", "3164522207", new DateTime(2023, 11, 29, 6, 35, 34, 119, DateTimeKind.Utc).AddTicks(2544), new Guid("00000000-0000-0000-0000-000000000000"), "世界频道", null });

            migrationBuilder.InsertData(
                table: "GroupUsers",
                columns: new[] { "GroupId", "UserId", "CreationTime", "Creator", "Grouper", "Id", "IsAdmin", "ModificationTime", "Modifier", "NickName" },
                values: new object[,]
                {
                    { new Guid("5bf589f0-91d2-8bca-285a-3a0f2bf71767"), new Guid("9387fcb3-409c-1f45-3b71-3a0f2bf71767"), new DateTime(2023, 11, 29, 14, 35, 34, 119, DateTimeKind.Local).AddTicks(2590), new Guid("00000000-0000-0000-0000-000000000000"), false, new Guid("a872a0c8-8657-52b4-31b4-3a0f2bf71767"), false, new DateTime(2023, 11, 29, 6, 35, 34, 119, DateTimeKind.Utc).AddTicks(2588), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("5bf589f0-91d2-8bca-285a-3a0f2bf71767"), new Guid("a31329c0-e0b1-6752-ec54-3a0f2bf71767"), new DateTime(2023, 11, 29, 14, 35, 34, 119, DateTimeKind.Local).AddTicks(2568), new Guid("00000000-0000-0000-0000-000000000000"), false, new Guid("7e198562-6d35-94b4-848e-3a0f2bf71767"), false, new DateTime(2023, 11, 29, 6, 35, 34, 119, DateTimeKind.Utc).AddTicks(2558), new Guid("00000000-0000-0000-0000-000000000000"), null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Account", "Autograph", "Avatar", "Email", "LoginType", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("9387fcb3-409c-1f45-3b71-3a0f2bf71767"), "3164522207", null, "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4", "3164522206@qq.com", 0, "wyg", "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4=" },
                    { new Guid("a31329c0-e0b1-6752-ec54-3a0f2bf71767"), "3164522206", null, "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4", "3164522206@qq.com", 0, "admin", "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4=" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ChatGroups",
                keyColumn: "Id",
                keyValue: new Guid("5bf589f0-91d2-8bca-285a-3a0f2bf71767"));

            migrationBuilder.DeleteData(
                table: "GroupUsers",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { new Guid("5bf589f0-91d2-8bca-285a-3a0f2bf71767"), new Guid("9387fcb3-409c-1f45-3b71-3a0f2bf71767") });

            migrationBuilder.DeleteData(
                table: "GroupUsers",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { new Guid("5bf589f0-91d2-8bca-285a-3a0f2bf71767"), new Guid("a31329c0-e0b1-6752-ec54-3a0f2bf71767") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9387fcb3-409c-1f45-3b71-3a0f2bf71767"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a31329c0-e0b1-6752-ec54-3a0f2bf71767"));

            migrationBuilder.DropColumn(
                name: "Withdraw",
                table: "ChatMessages");

            migrationBuilder.InsertData(
                table: "ChatGroups",
                columns: new[] { "Id", "Avatar", "CreationTime", "Creator", "Description", "GroupNumber", "ModificationTime", "Modifier", "Name", "UserId" },
                values: new object[] { new Guid("9a46221e-ee1a-e580-f079-3a0effb21773"), "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4", new DateTime(2023, 11, 20, 16, 16, 54, 643, DateTimeKind.Utc).AddTicks(5604), new Guid("00000000-0000-0000-0000-000000000000"), "世界频道欢迎来访", "3164522207", new DateTime(2023, 11, 20, 16, 16, 54, 643, DateTimeKind.Utc).AddTicks(5605), new Guid("00000000-0000-0000-0000-000000000000"), "世界频道", null });

            migrationBuilder.InsertData(
                table: "GroupUsers",
                columns: new[] { "GroupId", "UserId", "CreationTime", "Creator", "Grouper", "Id", "IsAdmin", "ModificationTime", "Modifier", "NickName" },
                values: new object[,]
                {
                    { new Guid("9a46221e-ee1a-e580-f079-3a0effb21773"), new Guid("cabed7f6-055a-6e31-5f86-3a0effb21773"), new DateTime(2023, 11, 21, 0, 16, 54, 643, DateTimeKind.Local).AddTicks(5652), new Guid("00000000-0000-0000-0000-000000000000"), false, new Guid("0bb1d58b-f431-386b-1f0f-3a0effb21773"), false, new DateTime(2023, 11, 20, 16, 16, 54, 643, DateTimeKind.Utc).AddTicks(5651), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("9a46221e-ee1a-e580-f079-3a0effb21773"), new Guid("dba9a606-c418-6396-8c8e-3a0effb21773"), new DateTime(2023, 11, 21, 0, 16, 54, 643, DateTimeKind.Local).AddTicks(5627), new Guid("00000000-0000-0000-0000-000000000000"), false, new Guid("07f54bbb-da84-6831-f3fe-3a0effb21773"), false, new DateTime(2023, 11, 20, 16, 16, 54, 643, DateTimeKind.Utc).AddTicks(5619), new Guid("00000000-0000-0000-0000-000000000000"), null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Account", "Autograph", "Avatar", "Email", "LoginType", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("cabed7f6-055a-6e31-5f86-3a0effb21773"), "3164522207", null, "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4", "3164522206@qq.com", 0, "wyg", "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4=" },
                    { new Guid("dba9a606-c418-6396-8c8e-3a0effb21773"), "3164522206", null, "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4", "3164522206@qq.com", 0, "admin", "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4=" }
                });
        }
    }
}
