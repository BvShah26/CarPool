using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class RideApprovalenumStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RequestTime",
                table: "RideApprovals",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RideApprovals",
                nullable: false,
                defaultValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "ClientUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestTime",
                table: "RideApprovals");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RideApprovals");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "ClientUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
