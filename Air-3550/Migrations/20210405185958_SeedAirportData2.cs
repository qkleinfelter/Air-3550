using Microsoft.EntityFrameworkCore.Migrations;

namespace Air_3550.Migrations
{
    public partial class SeedAirportData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 1,
                columns: new[] { "AirportCode", "City", "Elevation", "Latitude", "Longitude" },
                values: new object[] { "CLE", "Cleveland", 791, 41.411667m, -81.849722m });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 2, "ATL", null, "Atlanta", "USA", 1027, 33.636667m, -84.428056m, "Georgia" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 3, "LAX", null, "Los Angeles", "USA", 125, 33.9425m, -118.408056m, "California" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 4, "MDW", null, "Chicago", "USA", 620, 41.786111m, -87.7525m, "Illinois" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 5, "DAL", null, "Dallas", "USA", 486, 32.847222m, -96.851667m, "Texas" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 6, "DEN", null, "Denver", "USA", 5430, 39.861667m, -104.673056m, "Colorado" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 7, "LGA", null, "New York", "USA", 19, 40.775m, -73.875m, "New York" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 8, "MIA", null, "Miami", "USA", 9, 25.793333m, -80.290556m, "Florida" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 9, "SEA", null, "Seattle", "USA", 433, 47.448889m, -122.309444m, "Washington" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 10, "BNA", null, "Nashville", "USA", 599, 36.126667m, -86.681944m, "Tennessee" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Airports",
                keyColumn: "AirportId",
                keyValue: 1,
                columns: new[] { "AirportCode", "City", "Elevation", "Latitude", "Longitude" },
                values: new object[] { "TOL", "Toledo", 684, 41.586806m, -83.807833m });
        }
    }
}
