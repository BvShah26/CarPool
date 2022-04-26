using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class RemovedVehicleNumberUserLicense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarNumber",
                table: "Uservehicles");

            migrationBuilder.DropColumn(
                name: "IsCar_NumberDisclosed",
                table: "Uservehicles");

            migrationBuilder.DropColumn(
                name: "LicenseNumber",
                table: "ClientUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarNumber",
                table: "Uservehicles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCar_NumberDisclosed",
                table: "Uservehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LicenseNumber",
                table: "ClientUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
