using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class IsRejctedRideApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "RideApprovals",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "RideApprovals");
        }
    }
}
