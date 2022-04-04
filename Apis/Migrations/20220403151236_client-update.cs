using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class clientupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uservehicles_Users_UserId",
                table: "Uservehicles");

            migrationBuilder.DropIndex(
                name: "IX_Uservehicles_UserId",
                table: "Uservehicles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Uservehicles");

            migrationBuilder.AddColumn<int>(
                name: "UserOwnerId",
                table: "Uservehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Uservehicles_UserOwnerId",
                table: "Uservehicles",
                column: "UserOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uservehicles_Users_UserOwnerId",
                table: "Uservehicles",
                column: "UserOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uservehicles_Users_UserOwnerId",
                table: "Uservehicles");

            migrationBuilder.DropIndex(
                name: "IX_Uservehicles_UserOwnerId",
                table: "Uservehicles");

            migrationBuilder.DropColumn(
                name: "UserOwnerId",
                table: "Uservehicles");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Uservehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Uservehicles_UserId",
                table: "Uservehicles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uservehicles_Users_UserId",
                table: "Uservehicles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
