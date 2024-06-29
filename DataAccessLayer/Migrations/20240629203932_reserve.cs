using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class reserve : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Cars_CarID",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Locations_DropOffLocationID",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Locations_PickUpLocationID",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservation");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_PickUpLocationID",
                table: "Reservation",
                newName: "IX_Reservation_PickUpLocationID");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_DropOffLocationID",
                table: "Reservation",
                newName: "IX_Reservation_DropOffLocationID");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_CarID",
                table: "Reservation",
                newName: "IX_Reservation_CarID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "ReservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Cars_CarID",
                table: "Reservation",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "CarID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Locations_DropOffLocationID",
                table: "Reservation",
                column: "DropOffLocationID",
                principalTable: "Locations",
                principalColumn: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Locations_PickUpLocationID",
                table: "Reservation",
                column: "PickUpLocationID",
                principalTable: "Locations",
                principalColumn: "LocationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Cars_CarID",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Locations_DropOffLocationID",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Locations_PickUpLocationID",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservation");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_PickUpLocationID",
                table: "Reservation",
                newName: "IX_Reservation_PickUpLocationID");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_DropOffLocationID",
                table: "Reservation",
                newName: "IX_Reservation_DropOffLocationID");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_CarID",
                table: "Reservation",
                newName: "IX_Reservation_CarID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "ReservationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Cars_CarID",
                table: "Reservation",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "CarID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Locations_DropOffLocationID",
                table: "Reservation",
                column: "DropOffLocationID",
                principalTable: "Locations",
                principalColumn: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Locations_PickUpLocationID",
                table: "Reservation",
                column: "PickUpLocationID",
                principalTable: "Locations",
                principalColumn: "LocationID");
        }
    }
}
