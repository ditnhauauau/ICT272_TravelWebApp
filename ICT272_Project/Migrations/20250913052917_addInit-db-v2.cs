using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICT272_Project.Migrations
{
    /// <inheritdoc />
    public partial class addInitdbv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TourDate_TourDateID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_TourDate_TourPackages_PackageID",
                table: "TourDate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourDate",
                table: "TourDate");

            migrationBuilder.RenameTable(
                name: "TourDate",
                newName: "TourDates");

            migrationBuilder.RenameIndex(
                name: "IX_TourDate_PackageID",
                table: "TourDates",
                newName: "IX_TourDates_PackageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourDates",
                table: "TourDates",
                column: "TourDateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TourDates_TourDateID",
                table: "Bookings",
                column: "TourDateID",
                principalTable: "TourDates",
                principalColumn: "TourDateID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourDates_TourPackages_PackageID",
                table: "TourDates",
                column: "PackageID",
                principalTable: "TourPackages",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_TourDates_TourDateID",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_TourDates_TourPackages_PackageID",
                table: "TourDates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourDates",
                table: "TourDates");

            migrationBuilder.RenameTable(
                name: "TourDates",
                newName: "TourDate");

            migrationBuilder.RenameIndex(
                name: "IX_TourDates_PackageID",
                table: "TourDate",
                newName: "IX_TourDate_PackageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourDate",
                table: "TourDate",
                column: "TourDateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_TourDate_TourDateID",
                table: "Bookings",
                column: "TourDateID",
                principalTable: "TourDate",
                principalColumn: "TourDateID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourDate_TourPackages_PackageID",
                table: "TourDate",
                column: "PackageID",
                principalTable: "TourPackages",
                principalColumn: "PackageID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
