using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apis.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CancellationReasons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancellationReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreferenceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferenceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    MobileNumber = table.Column<long>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    LicenseNumber = table.Column<int>(nullable: false),
                    ProfileImage = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehcile_Brand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehcile_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle_Color",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TravelPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelPreferences_PreferenceTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PreferenceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehciles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    VehicleBrandId = table.Column<int>(nullable: false),
                    VehicleTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehciles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehciles_Vehcile_Brand_VehicleBrandId",
                        column: x => x.VehicleBrandId,
                        principalTable: "Vehcile_Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehciles_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_TravelPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Travel_PreferenceId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_TravelPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_TravelPreferences_TravelPreferences_Travel_PreferenceId",
                        column: x => x.Travel_PreferenceId,
                        principalTable: "TravelPreferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_TravelPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uservehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleImage = table.Column<string>(nullable: true),
                    IsCar_NumberDisclosed = table.Column<bool>(nullable: false),
                    CarNumber = table.Column<string>(nullable: true),
                    VehicleId = table.Column<int>(nullable: false),
                    ColorId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Manufacture_Year = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uservehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uservehicles_Vehicle_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Vehicle_Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uservehicles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uservehicles_Vehciles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehciles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publish_Rides",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherId = table.Column<int>(nullable: false),
                    VehicleId = table.Column<int>(nullable: false),
                    JourneyDate = table.Column<DateTime>(nullable: false),
                    MaxPassengers = table.Column<int>(nullable: false),
                    Departure_City = table.Column<string>(nullable: true),
                    Destination_City = table.Column<string>(nullable: true),
                    PickUp_Location = table.Column<string>(nullable: true),
                    DropOff_Location = table.Column<string>(nullable: true),
                    PickUp_Time = table.Column<DateTime>(nullable: false),
                    DropOff_Time = table.Column<DateTime>(nullable: false),
                    IsInstant_Approval = table.Column<bool>(nullable: false),
                    Price_Seat = table.Column<int>(nullable: false),
                    Ride_Note = table.Column<string>(nullable: true),
                    Publishing_Timestamp = table.Column<DateTime>(nullable: false),
                    IsCompletelyBooked = table.Column<bool>(nullable: false),
                    IsCancelled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publish_Rides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publish_Rides_Users_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);

                    table.ForeignKey(
                        name: "FK_Publish_Rides_Uservehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Uservehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Publish_RideId = table.Column<int>(nullable: false),
                    RiderId = table.Column<int>(nullable: false),
                    SeatQty = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false),
                    IsCancelled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Publish_Rides_Publish_RideId",
                        column: x => x.Publish_RideId,
                        principalTable: "Publish_Rides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_RiderId",
                        column: x => x.RiderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RideApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RideId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RideApprovals_Publish_Rides_RideId",
                        column: x => x.RideId,
                        principalTable: "Publish_Rides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RideApprovals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Publish_RideId",
                table: "Bookings",
                column: "Publish_RideId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RiderId",
                table: "Bookings",
                column: "RiderId");

            migrationBuilder.CreateIndex(
                name: "IX_Publish_Rides_PublisherId",
                table: "Publish_Rides",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Publish_Rides_VehicleId",
                table: "Publish_Rides",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_RideApprovals_RideId",
                table: "RideApprovals",
                column: "RideId");

            migrationBuilder.CreateIndex(
                name: "IX_RideApprovals_UserId",
                table: "RideApprovals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelPreferences_TypeId",
                table: "TravelPreferences",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TravelPreferences_Travel_PreferenceId",
                table: "User_TravelPreferences",
                column: "Travel_PreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_User_TravelPreferences_UserId",
                table: "User_TravelPreferences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Uservehicles_ColorId",
                table: "Uservehicles",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Uservehicles_UserId",
                table: "Uservehicles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Uservehicles_VehicleId",
                table: "Uservehicles",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehciles_VehicleBrandId",
                table: "Vehciles",
                column: "VehicleBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehciles_VehicleTypeId",
                table: "Vehciles",
                column: "VehicleTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "CancellationReasons");

            migrationBuilder.DropTable(
                name: "RideApprovals");

            migrationBuilder.DropTable(
                name: "User_TravelPreferences");

            migrationBuilder.DropTable(
                name: "Publish_Rides");

            migrationBuilder.DropTable(
                name: "TravelPreferences");

            migrationBuilder.DropTable(
                name: "Uservehicles");

            migrationBuilder.DropTable(
                name: "PreferenceTypes");

            migrationBuilder.DropTable(
                name: "Vehicle_Color");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vehciles");

            migrationBuilder.DropTable(
                name: "Vehcile_Brand");

            migrationBuilder.DropTable(
                name: "VehicleTypes");
        }
    }
}
