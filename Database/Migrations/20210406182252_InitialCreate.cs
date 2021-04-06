using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    AirportId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<decimal>(type: "Decimal(8,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "Decimal(9,6)", nullable: false),
                    Elevation = table.Column<int>(type: "INTEGER", nullable: false),
                    AirportCode = table.Column<string>(type: "TEXT", nullable: false),
                    AirportId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.AirportId);
                    table.ForeignKey(
                        name: "FK_Airports_Airports_AirportId1",
                        column: x => x.AirportId1,
                        principalTable: "Airports",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    PlaneId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    MaxSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxDistance = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.PlaneId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LoginId = table.Column<string>(type: "TEXT", nullable: false),
                    HashedPass = table.Column<string>(type: "TEXT", nullable: false),
                    UserRole = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OriginAirportId = table.Column<int>(type: "INTEGER", nullable: true),
                    DestinationAirportId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_Trips_Airports_DestinationAirportId",
                        column: x => x.DestinationAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Airports_OriginAirportId",
                        column: x => x.OriginAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OriginAirportId = table.Column<int>(type: "INTEGER", nullable: true),
                    DestinationAirportId = table.Column<int>(type: "INTEGER", nullable: true),
                    PlaneTypePlaneId = table.Column<int>(type: "INTEGER", nullable: true),
                    Cost = table.Column<int>(type: "INTEGER", nullable: false),
                    TicketsPurchased = table.Column<int>(type: "INTEGER", nullable: false),
                    FlightNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_DestinationAirportId",
                        column: x => x.DestinationAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_OriginAirportId",
                        column: x => x.OriginAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Planes_PlaneTypePlaneId",
                        column: x => x.PlaneTypePlaneId,
                        principalTable: "Planes",
                        principalColumn: "PlaneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: true),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentType = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TripId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 1, "CLE", null, "Cleveland", "USA", 791, 41.411667m, -81.849722m, "Ohio" });

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

            migrationBuilder.CreateIndex(
                name: "IX_Airports_AirportId1",
                table: "Airports",
                column: "AirportId1");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DestinationAirportId",
                table: "Flights",
                column: "DestinationAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_OriginAirportId",
                table: "Flights",
                column: "OriginAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PlaneTypePlaneId",
                table: "Flights",
                column: "PlaneTypePlaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightId",
                table: "Tickets",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TripId",
                table: "Tickets",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationAirportId",
                table: "Trips",
                column: "DestinationAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_OriginAirportId",
                table: "Trips",
                column: "OriginAirportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
