using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AdjustSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 1,
                columns: new[] { "Cost", "DepartureTime", "DestinationAirportId", "OriginAirportId", "TicketsPurchased" },
                values: new object[] { -1, new TimeSpan(0, 6, 10, 0, 0), 6, 9, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 83, -1, new TimeSpan(0, 14, 35, 0, 0), 2, 83, 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 82, -1, new TimeSpan(0, 10, 40, 0, 0), 2, 82, 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 81, -1, new TimeSpan(0, 8, 10, 0, 0), 2, 81, 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 80, -1, new TimeSpan(0, 19, 20, 0, 0), 10, 80, 1, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 79, -1, new TimeSpan(0, 15, 20, 0, 0), 10, 79, 1, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 78, -1, new TimeSpan(0, 9, 50, 0, 0), 10, 78, 1, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 77, -1, new TimeSpan(0, 7, 10, 0, 0), 10, 77, 1, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 76, -1, new TimeSpan(0, 18, 20, 0, 0), 1, 76, 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 75, -1, new TimeSpan(0, 14, 30, 0, 0), 1, 75, 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 74, -1, new TimeSpan(0, 11, 10, 0, 0), 1, 74, 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 73, -1, new TimeSpan(0, 8, 45, 0, 0), 1, 73, 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 84, -1, new TimeSpan(0, 17, 40, 0, 0), 2, 84, 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 72, -1, new TimeSpan(0, 18, 25, 0, 0), 4, 72, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 70, -1, new TimeSpan(0, 9, 35, 0, 0), 4, 70, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 69, -1, new TimeSpan(0, 6, 40, 0, 0), 4, 69, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 68, -1, new TimeSpan(0, 17, 35, 0, 0), 2, 68, 4, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 67, -1, new TimeSpan(0, 13, 40, 0, 0), 2, 67, 4, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 66, -1, new TimeSpan(0, 11, 10, 0, 0), 2, 66, 4, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 65, -1, new TimeSpan(0, 9, 0, 0, 0), 2, 65, 4, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 64, -1, new TimeSpan(0, 19, 20, 0, 0), 4, 64, 1, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 63, -1, new TimeSpan(0, 15, 35, 0, 0), 4, 63, 1, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 62, -1, new TimeSpan(0, 10, 10, 0, 0), 4, 62, 1, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 61, -1, new TimeSpan(0, 7, 0, 0, 0), 4, 61, 1, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 60, -1, new TimeSpan(0, 16, 30, 0, 0), 1, 60, 4, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 71, -1, new TimeSpan(0, 12, 10, 0, 0), 4, 71, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 59, -1, new TimeSpan(0, 12, 45, 0, 0), 1, 59, 4, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 85, -1, new TimeSpan(0, 8, 20, 0, 0), 10, 85, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 87, -1, new TimeSpan(0, 15, 20, 0, 0), 10, 87, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 111, -1, new TimeSpan(0, 13, 45, 0, 0), 2, 111, 8, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 110, -1, new TimeSpan(0, 11, 20, 0, 0), 2, 110, 8, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 109, -1, new TimeSpan(0, 9, 30, 0, 0), 2, 109, 8, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 108, -1, new TimeSpan(0, 18, 40, 0, 0), 8, 108, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 107, -1, new TimeSpan(0, 13, 45, 0, 0), 8, 107, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 106, -1, new TimeSpan(0, 10, 40, 0, 0), 8, 106, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 105, -1, new TimeSpan(0, 9, 10, 0, 0), 8, 105, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 104, -1, new TimeSpan(0, 17, 10, 0, 0), 2, 104, 7, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 103, -1, new TimeSpan(0, 14, 0, 0, 0), 2, 103, 7, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 102, -1, new TimeSpan(0, 11, 30, 0, 0), 2, 102, 7, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 101, -1, new TimeSpan(0, 9, 30, 0, 0), 2, 101, 7, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 86, -1, new TimeSpan(0, 9, 50, 0, 0), 10, 86, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 100, -1, new TimeSpan(0, 16, 40, 0, 0), 7, 100, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 98, -1, new TimeSpan(0, 11, 10, 0, 0), 7, 98, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 97, -1, new TimeSpan(0, 9, 0, 0, 0), 7, 97, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 96, -1, new TimeSpan(0, 18, 30, 0, 0), 1, 96, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 95, -1, new TimeSpan(0, 14, 5, 0, 0), 1, 95, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 94, -1, new TimeSpan(0, 11, 10, 0, 0), 1, 94, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 93, -1, new TimeSpan(0, 8, 5, 0, 0), 1, 93, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 92, -1, new TimeSpan(0, 19, 0, 0, 0), 2, 92, 1, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 91, -1, new TimeSpan(0, 13, 40, 0, 0), 2, 91, 1, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 90, -1, new TimeSpan(0, 9, 45, 0, 0), 2, 90, 1, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 89, -1, new TimeSpan(0, 8, 0, 0, 0), 2, 89, 1, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 88, -1, new TimeSpan(0, 20, 10, 0, 0), 10, 88, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 99, -1, new TimeSpan(0, 13, 50, 0, 0), 7, 99, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 58, -1, new TimeSpan(0, 9, 40, 0, 0), 1, 58, 4, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 57, -1, new TimeSpan(0, 7, 10, 0, 0), 1, 57, 4, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 56, -1, new TimeSpan(0, 18, 20, 0, 0), 6, 56, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 26, -1, new TimeSpan(0, 9, 40, 0, 0), 5, 26, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 25, -1, new TimeSpan(0, 7, 10, 0, 0), 5, 25, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 24, -1, new TimeSpan(0, 20, 50, 0, 0), 3, 24, 5, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 23, -1, new TimeSpan(0, 14, 10, 0, 0), 3, 23, 5, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 22, -1, new TimeSpan(0, 9, 45, 0, 0), 3, 22, 5, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 21, -1, new TimeSpan(0, 7, 30, 0, 0), 3, 21, 5, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 20, -1, new TimeSpan(0, 18, 10, 0, 0), 5, 20, 3, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 19, -1, new TimeSpan(0, 14, 50, 0, 0), 5, 19, 3, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 18, -1, new TimeSpan(0, 11, 40, 0, 0), 5, 18, 3, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 17, -1, new TimeSpan(0, 6, 10, 0, 0), 5, 17, 3, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 16, -1, new TimeSpan(0, 20, 35, 0, 0), 3, 16, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 27, -1, new TimeSpan(0, 14, 5, 0, 0), 5, 27, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 15, -1, new TimeSpan(0, 17, 0, 0, 0), 3, 15, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 13, -1, new TimeSpan(0, 8, 0, 0, 0), 3, 13, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 12, -1, new TimeSpan(0, 17, 55, 0, 0), 6, 12, 3, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 11, -1, new TimeSpan(0, 14, 25, 0, 0), 6, 11, 3, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 10, -1, new TimeSpan(0, 11, 55, 0, 0), 6, 10, 3, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 9, -1, new TimeSpan(0, 6, 5, 0, 0), 6, 9, 3, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 8, -1, new TimeSpan(0, 20, 55, 0, 0), 9, 8, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 7, -1, new TimeSpan(0, 15, 55, 0, 0), 9, 7, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 6, -1, new TimeSpan(0, 9, 30, 0, 0), 9, 6, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 5, -1, new TimeSpan(0, 6, 50, 0, 0), 9, 5, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 4, -1, new TimeSpan(0, 18, 35, 0, 0), 6, 4, 9, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 3, -1, new TimeSpan(0, 14, 10, 0, 0), 6, 3, 9, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 14, -1, new TimeSpan(0, 12, 55, 0, 0), 3, 14, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 28, -1, new TimeSpan(0, 18, 55, 0, 0), 5, 28, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 29, -1, new TimeSpan(0, 7, 10, 0, 0), 6, 29, 5, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 30, -1, new TimeSpan(0, 11, 10, 0, 0), 6, 30, 5, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 55, -1, new TimeSpan(0, 14, 40, 0, 0), 6, 55, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 54, -1, new TimeSpan(0, 11, 10, 0, 0), 6, 54, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 53, -1, new TimeSpan(0, 7, 20, 0, 0), 6, 53, 2, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 52, -1, new TimeSpan(0, 18, 40, 0, 0), 2, 52, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 51, -1, new TimeSpan(0, 14, 30, 0, 0), 2, 51, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 50, -1, new TimeSpan(0, 9, 15, 0, 0), 2, 50, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 49, -1, new TimeSpan(0, 7, 0, 0, 0), 2, 49, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 48, -1, new TimeSpan(0, 17, 50, 0, 0), 6, 48, 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 47, -1, new TimeSpan(0, 15, 30, 0, 0), 6, 47, 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 46, -1, new TimeSpan(0, 9, 5, 0, 0), 6, 46, 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 45, -1, new TimeSpan(0, 6, 40, 0, 0), 6, 45, 10, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 44, -1, new TimeSpan(0, 18, 50, 0, 0), 10, 44, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 43, -1, new TimeSpan(0, 15, 20, 0, 0), 10, 43, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 42, -1, new TimeSpan(0, 9, 15, 0, 0), 10, 42, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 41, -1, new TimeSpan(0, 6, 45, 0, 0), 10, 41, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 40, -1, new TimeSpan(0, 16, 45, 0, 0), 6, 40, 4, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 39, -1, new TimeSpan(0, 12, 45, 0, 0), 6, 39, 4, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 38, -1, new TimeSpan(0, 10, 30, 0, 0), 6, 38, 4, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 37, -1, new TimeSpan(0, 6, 50, 0, 0), 6, 37, 4, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 36, -1, new TimeSpan(0, 18, 50, 0, 0), 4, 36, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 35, -1, new TimeSpan(0, 13, 5, 0, 0), 4, 35, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 34, -1, new TimeSpan(0, 10, 55, 0, 0), 4, 34, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 33, -1, new TimeSpan(0, 8, 25, 0, 0), 4, 33, 6, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 32, -1, new TimeSpan(0, 20, 55, 0, 0), 6, 32, 5, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 31, -1, new TimeSpan(0, 16, 45, 0, 0), 6, 31, 5, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 112, -1, new TimeSpan(0, 17, 10, 0, 0), 2, 112, 8, 1, 0 });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 2, -1, new TimeSpan(0, 9, 35, 0, 0), 6, 2, 9, 1, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 112);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 1,
                columns: new[] { "Cost", "DepartureTime", "DestinationAirportId", "OriginAirportId", "TicketsPurchased" },
                values: new object[] { 75, new TimeSpan(0, 12, 0, 0, 0), 1, 10, 75 });
        }
    }
}
