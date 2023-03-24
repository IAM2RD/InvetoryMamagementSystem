using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minimal_API.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Items",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "CurrentUser",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CurrentUser",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
