using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Y.Chat.EntityCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNoticeGroupuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "Notices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "GroupUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Read",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "GroupUsers");
        }
    }
}
