using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class UserVehicleYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Manufacture_Year",
                table: "Uservehicles",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Manufacture_Year",
                table: "Uservehicles",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
