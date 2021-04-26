using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class RemoveUselessConnectedAirports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airports_Airports_AirportId1",
                table: "Airports");

            migrationBuilder.DropIndex(
                name: "IX_Airports_AirportId1",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "AirportId1",
                table: "Airports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirportId1",
                table: "Airports",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Airports_AirportId1",
                table: "Airports",
                column: "AirportId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Airports_Airports_AirportId1",
                table: "Airports",
                column: "AirportId1",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
