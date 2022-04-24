using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class Ratings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartnerRatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartnerId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ClientUsersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerRatings_ClientUsers_ClientUsersId",
                        column: x => x.ClientUsersId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartnerRatings_ClientUsers_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartnerRatings_ClientUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PublisherRatings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ClientUsersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublisherRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublisherRatings_ClientUsers_ClientUsersId",
                        column: x => x.ClientUsersId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublisherRatings_ClientUsers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PublisherRatings_ClientUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartnerRatings_ClientUsersId",
                table: "PartnerRatings",
                column: "ClientUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerRatings_PartnerId",
                table: "PartnerRatings",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerRatings_UserId",
                table: "PartnerRatings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherRatings_ClientUsersId",
                table: "PublisherRatings",
                column: "ClientUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherRatings_PublisherId",
                table: "PublisherRatings",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_PublisherRatings_UserId",
                table: "PublisherRatings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartnerRatings");

            migrationBuilder.DropTable(
                name: "PublisherRatings");
        }
    }
}
