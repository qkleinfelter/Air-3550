using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class NewInitialCreate : Migration
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
                name: "CustomerInfo",
                columns: table => new
                {
                    CustomerInfoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    Zip = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditCardNumber = table.Column<string>(type: "TEXT", nullable: false),
                    PointsUsed = table.Column<int>(type: "INTEGER", nullable: false),
                    PointsAvailable = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditBalance = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfo", x => x.CustomerInfoId);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    PlaneId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    MaxSeats = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.PlaneId);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OriginAirportId = table.Column<int>(type: "INTEGER", nullable: false),
                    DestinationAirportId = table.Column<int>(type: "INTEGER", nullable: false),
                    isCanceled = table.Column<bool>(type: "INTEGER", nullable: false),
                    totalCost = table.Column<int>(type: "INTEGER", nullable: false),
                    pointsClaimed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomerInfoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_Trips_Airports_DestinationAirportId",
                        column: x => x.DestinationAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_Airports_OriginAirportId",
                        column: x => x.OriginAirportId,
                        principalTable: "Airports",
                        principalColumn: "AirportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_CustomerInfo_CustomerInfoId",
                        column: x => x.CustomerInfoId,
                        principalTable: "CustomerInfo",
                        principalColumn: "CustomerInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LoginId = table.Column<string>(type: "TEXT", nullable: false),
                    HashedPass = table.Column<string>(type: "TEXT", nullable: false),
                    UserRole = table.Column<int>(type: "INTEGER", nullable: false),
                    CustInfoCustomerInfoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_CustomerInfo_CustInfoCustomerInfoId",
                        column: x => x.CustInfoCustomerInfoId,
                        principalTable: "CustomerInfo",
                        principalColumn: "CustomerInfoId",
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
                    DepartureTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    FlightNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    isCanceled = table.Column<bool>(type: "INTEGER", nullable: false)
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
                name: "ScheduledFlights",
                columns: table => new
                {
                    ScheduledFlightId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TicketsPurchased = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledFlights", x => x.ScheduledFlightId);
                    table.ForeignKey(
                        name: "FK_ScheduledFlights_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightScheduledFlightId = table.Column<int>(type: "INTEGER", nullable: true),
                    PaymentType = table.Column<int>(type: "INTEGER", nullable: false),
                    isCanceled = table.Column<bool>(type: "INTEGER", nullable: false),
                    TripId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_ScheduledFlights_FlightScheduledFlightId",
                        column: x => x.FlightScheduledFlightId,
                        principalTable: "ScheduledFlights",
                        principalColumn: "ScheduledFlightId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 1, "CLE", null, "Cleveland", "USA", 791, 41.411667m, -81.849722m, "Ohio" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 10, "BNA", null, "Nashville", "USA", 599, 36.126667m, -86.681944m, "Tennessee" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 8, "MIA", null, "Miami", "USA", 9, 25.793333m, -80.290556m, "Florida" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 7, "LGA", null, "New York", "USA", 19, 40.775m, -73.875m, "New York" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 6, "DEN", null, "Denver", "USA", 5430, 39.861667m, -104.673056m, "Colorado" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 9, "SEA", null, "Seattle", "USA", 433, 47.448889m, -122.309444m, "Washington" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 4, "MDW", null, "Chicago", "USA", 620, 41.786111m, -87.7525m, "Illinois" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 3, "LAX", null, "Los Angeles", "USA", 125, 33.9425m, -118.408056m, "California" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 2, "ATL", null, "Atlanta", "USA", 1027, 33.636667m, -84.428056m, "Georgia" });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "AirportId", "AirportCode", "AirportId1", "City", "Country", "Elevation", "Latitude", "Longitude", "State" },
                values: new object[] { 5, "DAL", null, "Dallas", "USA", 486, 32.847222m, -96.851667m, "Texas" });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxSeats", "Model" },
                values: new object[] { 1, 230, "Boeing 737 MAX" });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxSeats", "Model" },
                values: new object[] { 2, 416, "Boeing 747" });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxSeats", "Model" },
                values: new object[] { 3, 199, "Boeing 757" });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxSeats", "Model" },
                values: new object[] { 4, 550, "Boeing 777" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CustInfoCustomerInfoId", "HashedPass", "LoginId", "UserRole" },
                values: new object[] { 3, null, "ab8f196b4521a3aba1de420fe6ef552ce406d8551c3aef370f909cea85abbc77ca1d698ef31f659097eee16e365975047ff403df1aae6cc7ef54595c3ae4d172", "accounting_manager", 3 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CustInfoCustomerInfoId", "HashedPass", "LoginId", "UserRole" },
                values: new object[] { 1, null, "360caebd9edb68609c0933bade3565350e59e284cc503ce61bf0eebd42fb7e5bd657a71ed1498225168757e7f1095920411cce27779e0c778ec52535deae2040", "marketing_manager", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CustInfoCustomerInfoId", "HashedPass", "LoginId", "UserRole" },
                values: new object[] { 2, null, "41ec260efa3aa054a91cc6cf9441e3652637f75946c8fa6c2e926f289e095f46e62e19a28e02fb0fc25dd047b1f04e6e03cf930d464b540c40c0045eb6b7e252", "load_engineer", 2 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CustInfoCustomerInfoId", "HashedPass", "LoginId", "UserRole" },
                values: new object[] { 4, null, "e2289f3f3a66a81f5ffb52dff1a09cd2ae91a39eb248230d2907084679c6bacdb4a880da7835ca72003d8c37e107cf91c5f9795678303754eba1be42039bff4d", "flight_manager", 4 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 5, new TimeSpan(0, 6, 10, 0, 0), 6, 5, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1020, new TimeSpan(0, 9, 30, 0, 0), 2, 1020, 10, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1015, new TimeSpan(0, 8, 10, 0, 0), 2, 1015, 10, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1010, new TimeSpan(0, 6, 40, 0, 0), 2, 1010, 10, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1005, new TimeSpan(0, 5, 30, 0, 0), 2, 1005, 10, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 800, new TimeSpan(0, 19, 20, 0, 0), 4, 800, 1, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 795, new TimeSpan(0, 18, 0, 0, 0), 4, 795, 1, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1025, new TimeSpan(0, 10, 40, 0, 0), 2, 1025, 10, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 790, new TimeSpan(0, 15, 35, 0, 0), 4, 790, 1, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 780, new TimeSpan(0, 11, 50, 0, 0), 4, 780, 1, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 775, new TimeSpan(0, 10, 10, 0, 0), 4, 775, 1, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 770, new TimeSpan(0, 9, 20, 0, 0), 4, 770, 1, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 765, new TimeSpan(0, 8, 50, 0, 0), 4, 765, 1, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 760, new TimeSpan(0, 7, 0, 0, 0), 4, 760, 1, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 755, new TimeSpan(0, 5, 30, 0, 0), 4, 755, 1, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 785, new TimeSpan(0, 12, 40, 0, 0), 4, 785, 1, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1030, new TimeSpan(0, 11, 10, 0, 0), 2, 1030, 10, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1035, new TimeSpan(0, 12, 30, 0, 0), 2, 1035, 10, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1040, new TimeSpan(0, 13, 50, 0, 0), 2, 1040, 10, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 415, new TimeSpan(0, 9, 40, 0, 0), 4, 415, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 410, new TimeSpan(0, 8, 25, 0, 0), 4, 410, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 405, new TimeSpan(0, 6, 10, 0, 0), 4, 405, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1100, new TimeSpan(0, 20, 10, 0, 0), 10, 1100, 2, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1095, new TimeSpan(0, 18, 40, 0, 0), 10, 1095, 2, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1090, new TimeSpan(0, 15, 20, 0, 0), 10, 1090, 2, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1085, new TimeSpan(0, 12, 20, 0, 0), 10, 1085, 2, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1080, new TimeSpan(0, 11, 30, 0, 0), 10, 1080, 2, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1075, new TimeSpan(0, 10, 10, 0, 0), 10, 1075, 2, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1070, new TimeSpan(0, 9, 50, 0, 0), 10, 1070, 2, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1065, new TimeSpan(0, 8, 20, 0, 0), 10, 1065, 2, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1060, new TimeSpan(0, 6, 10, 0, 0), 10, 1060, 2, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1055, new TimeSpan(0, 5, 40, 0, 0), 10, 1055, 2, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1050, new TimeSpan(0, 17, 40, 0, 0), 2, 1050, 10, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1045, new TimeSpan(0, 14, 35, 0, 0), 2, 1045, 10, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 750, new TimeSpan(0, 18, 50, 0, 0), 1, 750, 4, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 745, new TimeSpan(0, 16, 30, 0, 0), 1, 745, 4, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 740, new TimeSpan(0, 14, 10, 0, 0), 1, 740, 4, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 735, new TimeSpan(0, 12, 45, 0, 0), 1, 735, 4, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1435, new TimeSpan(0, 12, 30, 0, 0), 5, 1435, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1430, new TimeSpan(0, 11, 10, 0, 0), 5, 1430, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1425, new TimeSpan(0, 9, 20, 0, 0), 5, 1425, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1420, new TimeSpan(0, 8, 10, 0, 0), 5, 1420, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1415, new TimeSpan(0, 7, 45, 0, 0), 5, 1415, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1410, new TimeSpan(0, 6, 30, 0, 0), 5, 1410, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1405, new TimeSpan(0, 5, 50, 0, 0), 5, 1405, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1200, new TimeSpan(0, 18, 30, 0, 0), 1, 1200, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1195, new TimeSpan(0, 16, 20, 0, 0), 1, 1195, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1190, new TimeSpan(0, 14, 5, 0, 0), 1, 1190, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1185, new TimeSpan(0, 12, 30, 0, 0), 1, 1185, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1180, new TimeSpan(0, 11, 10, 0, 0), 1, 1180, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1175, new TimeSpan(0, 10, 45, 0, 0), 1, 1175, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1170, new TimeSpan(0, 9, 30, 0, 0), 1, 1170, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1165, new TimeSpan(0, 8, 5, 0, 0), 1, 1165, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1440, new TimeSpan(0, 13, 10, 0, 0), 5, 1440, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 420, new TimeSpan(0, 10, 55, 0, 0), 4, 420, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1445, new TimeSpan(0, 15, 5, 0, 0), 5, 1445, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1455, new TimeSpan(0, 5, 40, 0, 0), 2, 1455, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 730, new TimeSpan(0, 11, 30, 0, 0), 1, 730, 4, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 725, new TimeSpan(0, 10, 10, 0, 0), 1, 725, 4, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 720, new TimeSpan(0, 9, 40, 0, 0), 1, 720, 4, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 715, new TimeSpan(0, 8, 20, 0, 0), 1, 715, 4, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 710, new TimeSpan(0, 7, 10, 0, 0), 1, 710, 4, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 705, new TimeSpan(0, 5, 40, 0, 0), 1, 705, 4, 3, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1500, new TimeSpan(0, 20, 10, 0, 0), 2, 1500, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1495, new TimeSpan(0, 18, 20, 0, 0), 2, 1495, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1490, new TimeSpan(0, 14, 45, 0, 0), 2, 1490, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1485, new TimeSpan(0, 12, 35, 0, 0), 2, 1485, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1480, new TimeSpan(0, 11, 50, 0, 0), 2, 1480, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1475, new TimeSpan(0, 10, 15, 0, 0), 2, 1475, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1470, new TimeSpan(0, 8, 10, 0, 0), 2, 1470, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1465, new TimeSpan(0, 7, 30, 0, 0), 2, 1465, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1460, new TimeSpan(0, 6, 20, 0, 0), 2, 1460, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1450, new TimeSpan(0, 17, 20, 0, 0), 5, 1450, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1160, new TimeSpan(0, 7, 10, 0, 0), 1, 1160, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 425, new TimeSpan(0, 11, 25, 0, 0), 4, 425, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 435, new TimeSpan(0, 13, 5, 0, 0), 4, 435, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1295, new TimeSpan(0, 17, 10, 0, 0), 2, 1295, 7, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1290, new TimeSpan(0, 14, 0, 0, 0), 2, 1290, 7, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1285, new TimeSpan(0, 12, 5, 0, 0), 2, 1285, 7, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1280, new TimeSpan(0, 11, 30, 0, 0), 2, 1280, 7, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1275, new TimeSpan(0, 10, 40, 0, 0), 2, 1275, 7, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1270, new TimeSpan(0, 9, 10, 0, 0), 2, 1270, 7, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1300, new TimeSpan(0, 19, 30, 0, 0), 2, 1300, 7, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1265, new TimeSpan(0, 7, 20, 0, 0), 2, 1265, 7, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1255, new TimeSpan(0, 5, 30, 0, 0), 2, 1255, 7, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1250, new TimeSpan(0, 18, 55, 0, 0), 7, 1250, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1245, new TimeSpan(0, 16, 40, 0, 0), 7, 1245, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1240, new TimeSpan(0, 14, 20, 0, 0), 7, 1240, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1235, new TimeSpan(0, 13, 50, 0, 0), 7, 1235, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1230, new TimeSpan(0, 12, 30, 0, 0), 7, 1230, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1260, new TimeSpan(0, 6, 40, 0, 0), 2, 1260, 7, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1305, new TimeSpan(0, 5, 40, 0, 0), 8, 1305, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1310, new TimeSpan(0, 6, 20, 0, 0), 8, 1310, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1315, new TimeSpan(0, 7, 45, 0, 0), 8, 1315, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1390, new TimeSpan(0, 15, 30, 0, 0), 2, 1390, 8, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1385, new TimeSpan(0, 13, 45, 0, 0), 2, 1385, 8, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1380, new TimeSpan(0, 12, 5, 0, 0), 2, 1380, 8, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1375, new TimeSpan(0, 11, 20, 0, 0), 2, 1375, 8, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1370, new TimeSpan(0, 9, 30, 0, 0), 2, 1370, 8, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1365, new TimeSpan(0, 8, 40, 0, 0), 2, 1365, 8, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1360, new TimeSpan(0, 7, 20, 0, 0), 2, 1360, 8, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1355, new TimeSpan(0, 6, 0, 0, 0), 2, 1355, 8, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1350, new TimeSpan(0, 20, 10, 0, 0), 8, 1350, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1345, new TimeSpan(0, 18, 40, 0, 0), 8, 1345, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1340, new TimeSpan(0, 15, 25, 0, 0), 8, 1340, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1335, new TimeSpan(0, 13, 45, 0, 0), 8, 1335, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1330, new TimeSpan(0, 11, 10, 0, 0), 8, 1330, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1325, new TimeSpan(0, 10, 40, 0, 0), 8, 1325, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1320, new TimeSpan(0, 9, 10, 0, 0), 8, 1320, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1225, new TimeSpan(0, 11, 10, 0, 0), 7, 1225, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1220, new TimeSpan(0, 10, 20, 0, 0), 7, 1220, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1215, new TimeSpan(0, 9, 0, 0, 0), 7, 1215, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1210, new TimeSpan(0, 7, 30, 0, 0), 7, 1210, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 610, new TimeSpan(0, 7, 0, 0, 0), 2, 610, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 605, new TimeSpan(0, 5, 20, 0, 0), 2, 605, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 500, new TimeSpan(0, 18, 0, 0, 0), 6, 500, 4, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 495, new TimeSpan(0, 16, 45, 0, 0), 6, 495, 4, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 490, new TimeSpan(0, 14, 20, 0, 0), 6, 490, 4, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 485, new TimeSpan(0, 12, 45, 0, 0), 6, 485, 4, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 480, new TimeSpan(0, 11, 50, 0, 0), 6, 480, 4, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 475, new TimeSpan(0, 10, 30, 0, 0), 6, 475, 4, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 470, new TimeSpan(0, 9, 20, 0, 0), 6, 470, 4, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 465, new TimeSpan(0, 8, 30, 0, 0), 6, 465, 4, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 460, new TimeSpan(0, 7, 10, 0, 0), 6, 460, 4, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 455, new TimeSpan(0, 6, 50, 0, 0), 6, 455, 4, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 450, new TimeSpan(0, 18, 50, 0, 0), 4, 450, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 445, new TimeSpan(0, 16, 10, 0, 0), 4, 445, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 440, new TimeSpan(0, 14, 50, 0, 0), 4, 440, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 615, new TimeSpan(0, 8, 10, 0, 0), 2, 615, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 430, new TimeSpan(0, 12, 0, 0, 0), 4, 430, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 620, new TimeSpan(0, 9, 15, 0, 0), 2, 620, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 630, new TimeSpan(0, 11, 20, 0, 0), 2, 630, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1205, new TimeSpan(0, 6, 10, 0, 0), 7, 1205, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 700, new TimeSpan(0, 20, 30, 0, 0), 6, 700, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 695, new TimeSpan(0, 18, 20, 0, 0), 6, 695, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 690, new TimeSpan(0, 15, 30, 0, 0), 6, 690, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 685, new TimeSpan(0, 14, 40, 0, 0), 6, 685, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 680, new TimeSpan(0, 11, 10, 0, 0), 6, 680, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 675, new TimeSpan(0, 10, 5, 0, 0), 6, 675, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 670, new TimeSpan(0, 9, 30, 0, 0), 6, 670, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 665, new TimeSpan(0, 8, 10, 0, 0), 6, 665, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 660, new TimeSpan(0, 7, 20, 0, 0), 6, 660, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 655, new TimeSpan(0, 5, 30, 0, 0), 6, 655, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 650, new TimeSpan(0, 18, 40, 0, 0), 2, 650, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 645, new TimeSpan(0, 15, 45, 0, 0), 2, 645, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 640, new TimeSpan(0, 14, 30, 0, 0), 2, 640, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 635, new TimeSpan(0, 12, 10, 0, 0), 2, 635, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 625, new TimeSpan(0, 10, 30, 0, 0), 2, 625, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1155, new TimeSpan(0, 5, 50, 0, 0), 1, 1155, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1150, new TimeSpan(0, 21, 0, 0, 0), 2, 1150, 1, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1145, new TimeSpan(0, 19, 0, 0, 0), 2, 1145, 1, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 965, new TimeSpan(0, 9, 50, 0, 0), 10, 965, 1, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 960, new TimeSpan(0, 8, 30, 0, 0), 10, 960, 1, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 955, new TimeSpan(0, 7, 10, 0, 0), 10, 955, 1, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 950, new TimeSpan(0, 18, 20, 0, 0), 1, 950, 10, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 945, new TimeSpan(0, 16, 10, 0, 0), 1, 945, 10, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 940, new TimeSpan(0, 14, 30, 0, 0), 1, 940, 10, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 970, new TimeSpan(0, 10, 40, 0, 0), 10, 970, 1, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 935, new TimeSpan(0, 13, 50, 0, 0), 1, 935, 10, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 925, new TimeSpan(0, 10, 20, 0, 0), 1, 925, 10, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 920, new TimeSpan(0, 9, 5, 0, 0), 1, 920, 10, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 915, new TimeSpan(0, 8, 45, 0, 0), 1, 915, 10, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 910, new TimeSpan(0, 7, 30, 0, 0), 1, 910, 10, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 905, new TimeSpan(0, 6, 10, 0, 0), 1, 905, 10, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 400, new TimeSpan(0, 20, 55, 0, 0), 6, 400, 5, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 930, new TimeSpan(0, 11, 10, 0, 0), 1, 930, 10, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 975, new TimeSpan(0, 11, 10, 0, 0), 10, 975, 1, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 980, new TimeSpan(0, 12, 20, 0, 0), 10, 980, 1, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 985, new TimeSpan(0, 14, 30, 0, 0), 10, 985, 1, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 160, new TimeSpan(0, 9, 20, 0, 0), 3, 160, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 155, new TimeSpan(0, 8, 0, 0, 0), 3, 155, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 150, new TimeSpan(0, 21, 5, 0, 0), 6, 150, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 145, new TimeSpan(0, 17, 55, 0, 0), 6, 145, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 140, new TimeSpan(0, 14, 25, 0, 0), 6, 140, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 135, new TimeSpan(0, 13, 5, 0, 0), 6, 135, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 130, new TimeSpan(0, 11, 55, 0, 0), 6, 130, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 125, new TimeSpan(0, 10, 30, 0, 0), 6, 125, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 120, new TimeSpan(0, 9, 40, 0, 0), 6, 120, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 115, new TimeSpan(0, 8, 50, 0, 0), 6, 115, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 110, new TimeSpan(0, 7, 20, 0, 0), 6, 110, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 105, new TimeSpan(0, 6, 5, 0, 0), 6, 105, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1000, new TimeSpan(0, 21, 0, 0, 0), 10, 1000, 1, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 995, new TimeSpan(0, 19, 20, 0, 0), 10, 995, 1, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 990, new TimeSpan(0, 15, 20, 0, 0), 10, 990, 1, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 395, new TimeSpan(0, 17, 30, 0, 0), 6, 395, 5, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 390, new TimeSpan(0, 16, 45, 0, 0), 6, 390, 5, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 385, new TimeSpan(0, 13, 5, 0, 0), 6, 385, 5, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 380, new TimeSpan(0, 12, 50, 0, 0), 6, 380, 5, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 80, new TimeSpan(0, 11, 30, 0, 0), 9, 80, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 75, new TimeSpan(0, 10, 20, 0, 0), 9, 75, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 70, new TimeSpan(0, 9, 55, 0, 0), 9, 70, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 65, new TimeSpan(0, 8, 10, 0, 0), 9, 65, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 60, new TimeSpan(0, 7, 30, 0, 0), 9, 60, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 55, new TimeSpan(0, 6, 50, 0, 0), 9, 55, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 50, new TimeSpan(0, 18, 35, 0, 0), 6, 50, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 45, new TimeSpan(0, 15, 30, 0, 0), 6, 45, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 40, new TimeSpan(0, 14, 10, 0, 0), 6, 40, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 35, new TimeSpan(0, 12, 10, 0, 0), 6, 35, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 30, new TimeSpan(0, 11, 40, 0, 0), 6, 30, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 25, new TimeSpan(0, 10, 15, 0, 0), 6, 25, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 20, new TimeSpan(0, 9, 35, 0, 0), 6, 20, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 15, new TimeSpan(0, 8, 10, 0, 0), 6, 15, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 10, new TimeSpan(0, 7, 20, 0, 0), 6, 10, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 85, new TimeSpan(0, 12, 40, 0, 0), 9, 85, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 165, new TimeSpan(0, 10, 30, 0, 0), 3, 165, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 90, new TimeSpan(0, 13, 10, 0, 0), 9, 90, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 100, new TimeSpan(0, 20, 55, 0, 0), 9, 100, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 375, new TimeSpan(0, 11, 10, 0, 0), 6, 375, 5, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 370, new TimeSpan(0, 10, 30, 0, 0), 6, 370, 5, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 365, new TimeSpan(0, 9, 20, 0, 0), 6, 365, 5, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 360, new TimeSpan(0, 8, 50, 0, 0), 6, 360, 5, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 355, new TimeSpan(0, 7, 10, 0, 0), 6, 355, 5, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 350, new TimeSpan(0, 21, 10, 0, 0), 5, 350, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 345, new TimeSpan(0, 18, 55, 0, 0), 5, 345, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 340, new TimeSpan(0, 15, 10, 0, 0), 5, 340, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 335, new TimeSpan(0, 14, 5, 0, 0), 5, 335, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 330, new TimeSpan(0, 12, 5, 0, 0), 5, 330, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 325, new TimeSpan(0, 11, 55, 0, 0), 5, 325, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 320, new TimeSpan(0, 10, 30, 0, 0), 5, 320, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 315, new TimeSpan(0, 9, 40, 0, 0), 5, 315, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 310, new TimeSpan(0, 8, 20, 0, 0), 5, 310, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 305, new TimeSpan(0, 7, 10, 0, 0), 5, 305, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 95, new TimeSpan(0, 15, 5, 0, 0), 9, 95, 6, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 170, new TimeSpan(0, 11, 5, 0, 0), 3, 170, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 175, new TimeSpan(0, 12, 55, 0, 0), 3, 175, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 180, new TimeSpan(0, 13, 10, 0, 0), 3, 180, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 845, new TimeSpan(0, 17, 35, 0, 0), 2, 845, 4, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 840, new TimeSpan(0, 15, 20, 0, 0), 2, 840, 4, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 835, new TimeSpan(0, 13, 40, 0, 0), 2, 835, 4, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 830, new TimeSpan(0, 12, 30, 0, 0), 2, 830, 4, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 825, new TimeSpan(0, 11, 10, 0, 0), 2, 825, 4, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 820, new TimeSpan(0, 10, 30, 0, 0), 2, 820, 4, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 815, new TimeSpan(0, 9, 0, 0, 0), 2, 815, 4, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 810, new TimeSpan(0, 7, 10, 0, 0), 2, 810, 4, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 805, new TimeSpan(0, 5, 45, 0, 0), 2, 805, 4, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 600, new TimeSpan(0, 19, 20, 0, 0), 6, 600, 10, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 595, new TimeSpan(0, 17, 50, 0, 0), 6, 595, 10, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 590, new TimeSpan(0, 15, 30, 0, 0), 6, 590, 10, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 585, new TimeSpan(0, 12, 40, 0, 0), 6, 585, 10, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 580, new TimeSpan(0, 11, 10, 0, 0), 6, 580, 10, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 575, new TimeSpan(0, 10, 30, 0, 0), 6, 575, 10, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 850, new TimeSpan(0, 19, 0, 0, 0), 2, 850, 4, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 570, new TimeSpan(0, 9, 5, 0, 0), 6, 570, 10, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 855, new TimeSpan(0, 5, 30, 0, 0), 4, 855, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 865, new TimeSpan(0, 7, 20, 0, 0), 4, 865, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1140, new TimeSpan(0, 15, 10, 0, 0), 2, 1140, 1, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1135, new TimeSpan(0, 13, 40, 0, 0), 2, 1135, 1, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1130, new TimeSpan(0, 11, 20, 0, 0), 2, 1130, 1, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1125, new TimeSpan(0, 10, 30, 0, 0), 2, 1125, 1, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1120, new TimeSpan(0, 9, 45, 0, 0), 2, 1120, 1, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1115, new TimeSpan(0, 8, 0, 0, 0), 2, 1115, 1, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1110, new TimeSpan(0, 7, 10, 0, 0), 2, 1110, 1, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1105, new TimeSpan(0, 5, 50, 0, 0), 2, 1105, 1, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 900, new TimeSpan(0, 18, 25, 0, 0), 4, 900, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 895, new TimeSpan(0, 14, 30, 0, 0), 4, 895, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 890, new TimeSpan(0, 12, 10, 0, 0), 4, 890, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 885, new TimeSpan(0, 11, 40, 0, 0), 4, 885, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 880, new TimeSpan(0, 10, 50, 0, 0), 4, 880, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 875, new TimeSpan(0, 9, 35, 0, 0), 4, 875, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 870, new TimeSpan(0, 8, 10, 0, 0), 4, 870, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 860, new TimeSpan(0, 6, 40, 0, 0), 4, 860, 2, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1395, new TimeSpan(0, 17, 10, 0, 0), 2, 1395, 8, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 565, new TimeSpan(0, 8, 50, 0, 0), 6, 565, 10, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 555, new TimeSpan(0, 6, 40, 0, 0), 6, 555, 10, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 255, new TimeSpan(0, 7, 30, 0, 0), 3, 255, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 250, new TimeSpan(0, 20, 50, 0, 0), 5, 250, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 245, new TimeSpan(0, 18, 10, 0, 0), 5, 245, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 240, new TimeSpan(0, 14, 50, 0, 0), 5, 240, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 235, new TimeSpan(0, 12, 10, 0, 0), 5, 235, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 230, new TimeSpan(0, 11, 40, 0, 0), 5, 230, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 225, new TimeSpan(0, 10, 30, 0, 0), 5, 225, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 220, new TimeSpan(0, 9, 5, 0, 0), 5, 220, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 215, new TimeSpan(0, 8, 20, 0, 0), 5, 215, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 210, new TimeSpan(0, 7, 40, 0, 0), 5, 210, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 205, new TimeSpan(0, 6, 10, 0, 0), 5, 205, 3, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 200, new TimeSpan(0, 20, 35, 0, 0), 3, 200, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 195, new TimeSpan(0, 19, 5, 0, 0), 3, 195, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 190, new TimeSpan(0, 17, 0, 0, 0), 3, 190, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 185, new TimeSpan(0, 14, 30, 0, 0), 3, 185, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 260, new TimeSpan(0, 8, 50, 0, 0), 3, 260, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 560, new TimeSpan(0, 7, 20, 0, 0), 6, 560, 10, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 265, new TimeSpan(0, 9, 45, 0, 0), 3, 265, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 275, new TimeSpan(0, 11, 20, 0, 0), 3, 275, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 550, new TimeSpan(0, 18, 50, 0, 0), 10, 550, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 545, new TimeSpan(0, 15, 20, 0, 0), 10, 545, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 540, new TimeSpan(0, 13, 30, 0, 0), 10, 540, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 535, new TimeSpan(0, 12, 5, 0, 0), 10, 535, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 530, new TimeSpan(0, 11, 45, 0, 0), 10, 530, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 525, new TimeSpan(0, 10, 30, 0, 0), 10, 525, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 520, new TimeSpan(0, 9, 15, 0, 0), 10, 520, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 515, new TimeSpan(0, 8, 20, 0, 0), 10, 515, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 510, new TimeSpan(0, 7, 10, 0, 0), 10, 510, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 505, new TimeSpan(0, 6, 45, 0, 0), 10, 505, 6, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 300, new TimeSpan(0, 23, 5, 0, 0), 3, 300, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 295, new TimeSpan(0, 20, 50, 0, 0), 3, 295, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 290, new TimeSpan(0, 15, 55, 0, 0), 3, 290, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 285, new TimeSpan(0, 14, 10, 0, 0), 3, 285, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 280, new TimeSpan(0, 12, 30, 0, 0), 3, 280, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 270, new TimeSpan(0, 10, 5, 0, 0), 3, 270, 5, 2, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 1400, new TimeSpan(0, 19, 55, 0, 0), 2, 1400, 8, 4, false });

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
                name: "IX_ScheduledFlights_FlightId",
                table: "ScheduledFlights",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightScheduledFlightId",
                table: "Tickets",
                column: "FlightScheduledFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TripId",
                table: "Tickets",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CustomerInfoId",
                table: "Trips",
                column: "CustomerInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationAirportId",
                table: "Trips",
                column: "DestinationAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_OriginAirportId",
                table: "Trips",
                column: "OriginAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustInfoCustomerInfoId",
                table: "Users",
                column: "CustInfoCustomerInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ScheduledFlights");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "CustomerInfo");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "Planes");
        }
    }
}
