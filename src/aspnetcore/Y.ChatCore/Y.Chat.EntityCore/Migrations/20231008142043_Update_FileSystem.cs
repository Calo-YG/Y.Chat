using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Y.Chat.EntityCore.Migrations
{
    /// <inheritdoc />
    public partial class Update_FileSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileSystems_ChatGroups_GroupId",
                table: "FileSystems");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "FileSystems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "FileType",
                table: "FileSystems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MinioName",
                table: "FileSystems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_FileSystems_ChatGroups_GroupId",
                table: "FileSystems",
                column: "GroupId",
                principalTable: "ChatGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileSystems_ChatGroups_GroupId",
                table: "FileSystems");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "FileSystems");

            migrationBuilder.DropColumn(
                name: "MinioName",
                table: "FileSystems");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "FileSystems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileSystems_ChatGroups_GroupId",
                table: "FileSystems",
                column: "GroupId",
                principalTable: "ChatGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
