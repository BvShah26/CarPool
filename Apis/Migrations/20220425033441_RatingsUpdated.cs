using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class RatingsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "PublisherRatings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "PartnerRatings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "PublisherRatings");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "PartnerRatings");
        }
    }
}
