using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Y.Chat.EntityCore.Migrations
{
    /// <inheritdoc />
    public partial class Add_Table_SystemMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatType",
                table: "ChatMessages");

            migrationBuilder.CreateTable(
                name: "SystemMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotfiyType = table.Column<int>(type: "int", nullable: false),
                    RecivedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemMessages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemMessages");

            migrationBuilder.AddColumn<int>(
                name: "ChatType",
                table: "ChatMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
