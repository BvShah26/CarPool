using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class ColorTableNewField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "Vehicle_Color",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "Vehicle_Color");
        }
    }
}
