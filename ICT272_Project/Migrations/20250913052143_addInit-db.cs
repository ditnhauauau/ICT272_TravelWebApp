using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICT272_Project.Migrations
{
    /// <inheritdoc />
    public partial class addInitdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TourPackages",
                columns: table => new
                {
                    PackageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MaxGroupSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPackages", x => x.PackageID);
                });

            migrationBuilder.CreateTable(
                name: "TourDate",
                columns: table => new
                {
                    TourDateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    AvailableDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourDate", x => x.TourDateID);
                    table.ForeignKey(
                        name: "FK_TourDate_TourPackages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "TourPackages",
                        principalColumn: "PackageID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TravelAgencies",
                columns: table => new
                {
                    AgencyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicesOffered = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TourPackagePackageID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelAgencies", x => x.AgencyID);
                    table.ForeignKey(
                        name: "FK_TravelAgencies_TourPackages_TourPackagePackageID",
                        column: x => x.TourPackagePackageID,
                        principalTable: "TourPackages",
                        principalColumn: "PackageID");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TouristID = table.Column<int>(type: "int", nullable: false),
                    TourDateID = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_TourDate_TourDateID",
                        column: x => x.TourDateID,
                        principalTable: "TourDate",
                        principalColumn: "TourDateID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Tourists_TouristID",
                        column: x => x.TouristID,
                        principalTable: "Tourists",
                        principalColumn: "TouristID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_BookingID",
                table: "Feedbacks",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TourDateID",
                table: "Bookings",
                column: "TourDateID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TouristID",
                table: "Bookings",
                column: "TouristID");

            migrationBuilder.CreateIndex(
                name: "IX_TourDate_PackageID",
                table: "TourDate",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_TravelAgencies_TourPackagePackageID",
                table: "TravelAgencies",
                column: "TourPackagePackageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Bookings_BookingID",
                table: "Feedbacks",
                column: "BookingID",
                principalTable: "Bookings",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Bookings_BookingID",
                table: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "TravelAgencies");

            migrationBuilder.DropTable(
                name: "TourDate");

            migrationBuilder.DropTable(
                name: "TourPackages");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_BookingID",
                table: "Feedbacks");
        }
    }
}
