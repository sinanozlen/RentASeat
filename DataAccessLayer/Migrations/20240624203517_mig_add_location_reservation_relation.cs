using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_location_reservation_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentACars_Cars_CarID",
                table: "RentACars");

            migrationBuilder.DropForeignKey(
                name: "FK_RentACars_Locations_LocationID",
                table: "RentACars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentACars",
                table: "RentACars");

            migrationBuilder.RenameTable(
                name: "RentACars",
                newName: "RentACar");

            migrationBuilder.RenameIndex(
                name: "IX_RentACars_LocationID",
                table: "RentACar",
                newName: "IX_RentACar_LocationID");

            migrationBuilder.RenameIndex(
                name: "IX_RentACars_CarID",
                table: "RentACar",
                newName: "IX_RentACar_CarID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentACar",
                table: "RentACar",
                column: "RentACarId");

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickUpLocationID = table.Column<int>(type: "int", nullable: true),
                    DropOffLocationID = table.Column<int>(type: "int", nullable: true),
                    CarID = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    DriverLicenseYear = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservation_Cars_CarID",
                        column: x => x.CarID,
                        principalTable: "Cars",
                        principalColumn: "CarID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_Locations_DropOffLocationID",
                        column: x => x.DropOffLocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID");
                    table.ForeignKey(
                        name: "FK_Reservation_Locations_PickUpLocationID",
                        column: x => x.PickUpLocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CarID",
                table: "Reservation",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_DropOffLocationID",
                table: "Reservation",
                column: "DropOffLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PickUpLocationID",
                table: "Reservation",
                column: "PickUpLocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_RentACar_Cars_CarID",
                table: "RentACar",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "CarID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentACar_Locations_LocationID",
                table: "RentACar",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentACar_Cars_CarID",
                table: "RentACar");

            migrationBuilder.DropForeignKey(
                name: "FK_RentACar_Locations_LocationID",
                table: "RentACar");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RentACar",
                table: "RentACar");

            migrationBuilder.RenameTable(
                name: "RentACar",
                newName: "RentACars");

            migrationBuilder.RenameIndex(
                name: "IX_RentACar_LocationID",
                table: "RentACars",
                newName: "IX_RentACars_LocationID");

            migrationBuilder.RenameIndex(
                name: "IX_RentACar_CarID",
                table: "RentACars",
                newName: "IX_RentACars_CarID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentACars",
                table: "RentACars",
                column: "RentACarId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentACars_Cars_CarID",
                table: "RentACars",
                column: "CarID",
                principalTable: "Cars",
                principalColumn: "CarID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentACars_Locations_LocationID",
                table: "RentACars",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
