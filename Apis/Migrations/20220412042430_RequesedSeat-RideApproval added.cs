using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class RequesedSeatRideApprovaladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequestedSeats",
                table: "RideApprovals",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedSeats",
                table: "RideApprovals");
        }
    }
}
