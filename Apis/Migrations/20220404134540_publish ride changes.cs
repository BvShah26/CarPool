using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class publishridechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DropOff_LatLong",
                table: "Publish_Rides",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickUp_LatLong",
                table: "Publish_Rides",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropOff_LatLong",
                table: "Publish_Rides");

            migrationBuilder.DropColumn(
                name: "PickUp_LatLong",
                table: "Publish_Rides");
        }
    }
}
