using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class RideApprovalRemovedMultipleStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "RideApprovals");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "RideApprovals");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "RideApprovals",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestTime",
                table: "RideApprovals",
                nullable: true,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "RideApprovals",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestTime",
                table: "RideApprovals",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "RideApprovals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "RideApprovals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
