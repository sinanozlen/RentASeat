using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class add_relationship_RentACar_CarPricing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarPricingID",
                table: "RentACars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RentACars_CarPricingID",
                table: "RentACars",
                column: "CarPricingID");

            migrationBuilder.AddForeignKey(
                name: "FK_RentACars_CarPricings_CarPricingID",
                table: "RentACars",
                column: "CarPricingID",
                principalTable: "CarPricings",
                principalColumn: "CarPricingID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentACars_CarPricings_CarPricingID",
                table: "RentACars");

            migrationBuilder.DropIndex(
                name: "IX_RentACars_CarPricingID",
                table: "RentACars");

            migrationBuilder.DropColumn(
                name: "CarPricingID",
                table: "RentACars");
        }
    }
}
