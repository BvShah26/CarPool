using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class ChatAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRoom",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiderId = table.Column<int>(nullable: false),
                    PublisherId = table.Column<int>(nullable: false),
                    RideId = table.Column<int>(nullable: false),
                    ClientUsersId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatRoom_ClientUsers_ClientUsersId",
                        column: x => x.ClientUsersId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatRoom_ClientUsers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatRoom_Publish_Rides_RideId",
                        column: x => x.RideId,
                        principalTable: "Publish_Rides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatRoom_ClientUsers_RiderId",
                        column: x => x.RiderId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatRoom_RoomId",
                        column: x => x.RoomId,
                        principalTable: "ChatRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_RoomId",
                table: "ChatMessages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoom_ClientUsersId",
                table: "ChatRoom",
                column: "ClientUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoom_PublisherId",
                table: "ChatRoom",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoom_RideId",
                table: "ChatRoom",
                column: "RideId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoom_RiderId",
                table: "ChatRoom",
                column: "RiderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "ChatRoom");
        }
    }
}
