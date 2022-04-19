using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class BookingCancelDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingCancellation_Bookings_BookingId",
                table: "BookingCancellation");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingCancellation_CancellationReasons_ReasonId",
                table: "BookingCancellation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingCancellation",
                table: "BookingCancellation");

            migrationBuilder.RenameTable(
                name: "BookingCancellation",
                newName: "bookingCancellations");

            migrationBuilder.RenameIndex(
                name: "IX_BookingCancellation_ReasonId",
                table: "bookingCancellations",
                newName: "IX_bookingCancellations_ReasonId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingCancellation_BookingId",
                table: "bookingCancellations",
                newName: "IX_bookingCancellations_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookingCancellations",
                table: "bookingCancellations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookingCancellations_Bookings_BookingId",
                table: "bookingCancellations",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookingCancellations_CancellationReasons_ReasonId",
                table: "bookingCancellations",
                column: "ReasonId",
                principalTable: "CancellationReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookingCancellations_Bookings_BookingId",
                table: "bookingCancellations");

            migrationBuilder.DropForeignKey(
                name: "FK_bookingCancellations_CancellationReasons_ReasonId",
                table: "bookingCancellations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookingCancellations",
                table: "bookingCancellations");

            migrationBuilder.RenameTable(
                name: "bookingCancellations",
                newName: "BookingCancellation");

            migrationBuilder.RenameIndex(
                name: "IX_bookingCancellations_ReasonId",
                table: "BookingCancellation",
                newName: "IX_BookingCancellation_ReasonId");

            migrationBuilder.RenameIndex(
                name: "IX_bookingCancellations_BookingId",
                table: "BookingCancellation",
                newName: "IX_BookingCancellation_BookingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingCancellation",
                table: "BookingCancellation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCancellation_Bookings_BookingId",
                table: "BookingCancellation",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCancellation_CancellationReasons_ReasonId",
                table: "BookingCancellation",
                column: "ReasonId",
                principalTable: "CancellationReasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
