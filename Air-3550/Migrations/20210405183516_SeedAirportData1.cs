using Microsoft.EntityFrameworkCore.Migrations;

namespace Air_3550.Migrations
{
    public partial class SeedAirportData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 1, "TOL", null, "Toledo", "USA", 684, 41.586806m, -83.807833m, "Ohio" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 1);
        }
    }
}
