using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class VehicleTableNamechaged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uservehicles_Vehciles_VehicleId",
                table: "Uservehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehciles_Vehcile_Brand_VehicleBrandId",
                table: "Vehciles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehciles_VehicleTypes_VehicleTypeId",
                table: "Vehciles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehciles",
                table: "Vehciles");

            migrationBuilder.RenameTable(
                name: "Vehciles",
                newName: "Vehicles");

            migrationBuilder.RenameIndex(
                name: "IX_Vehciles_VehicleTypeId",
                table: "Vehicles",
                newName: "IX_Vehicles_VehicleTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehciles_VehicleBrandId",
                table: "Vehicles",
                newName: "IX_Vehicles_VehicleBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Uservehicles_Vehicles_VehicleId",
                table: "Uservehicles",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Vehcile_Brand_VehicleBrandId",
                table: "Vehicles",
                column: "VehicleBrandId",
                principalTable: "Vehcile_Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleTypes_VehicleTypeId",
                table: "Vehicles",
                column: "VehicleTypeId",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uservehicles_Vehicles_VehicleId",
                table: "Uservehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Vehcile_Brand_VehicleBrandId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleTypes_VehicleTypeId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehciles");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_VehicleTypeId",
                table: "Vehciles",
                newName: "IX_Vehciles_VehicleTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_VehicleBrandId",
                table: "Vehciles",
                newName: "IX_Vehciles_VehicleBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehciles",
                table: "Vehciles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Uservehicles_Vehciles_VehicleId",
                table: "Uservehicles",
                column: "VehicleId",
                principalTable: "Vehciles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehciles_Vehcile_Brand_VehicleBrandId",
                table: "Vehciles",
                column: "VehicleBrandId",
                principalTable: "Vehcile_Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehciles_VehicleTypes_VehicleTypeId",
                table: "Vehciles",
                column: "VehicleTypeId",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
