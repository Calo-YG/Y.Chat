using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Y.Chat.EntityCore.Migrations
{
    /// <inheritdoc />
    public partial class Update_FileSystem_ChatGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Bachelors",
                table: "ChatGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "GroupNumber",
                table: "ChatGroups",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bachelors",
                table: "ChatGroups");

            migrationBuilder.DropColumn(
                name: "GroupNumber",
                table: "ChatGroups");
        }
    }
}
