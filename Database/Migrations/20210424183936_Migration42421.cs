using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class Migration42421 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 121, new TimeSpan(0, 7, 20, 0, 0), 6, 121, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 122, new TimeSpan(0, 8, 10, 0, 0), 6, 122, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 123, new TimeSpan(0, 10, 15, 0, 0), 6, 123, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 124, new TimeSpan(0, 11, 40, 0, 0), 6, 124, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 125, new TimeSpan(0, 12, 10, 0, 0), 6, 125, 9, 1, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 126, new TimeSpan(0, 8, 10, 0, 0), 2, 126, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 127, new TimeSpan(0, 10, 30, 0, 0), 2, 127, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 128, new TimeSpan(0, 11, 20, 0, 0), 2, 128, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 129, new TimeSpan(0, 12, 10, 0, 0), 2, 129, 6, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 130, new TimeSpan(0, 10, 20, 0, 0), 7, 130, 2, 4, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "isCanceled" },
                values: new object[] { 131, new TimeSpan(0, 12, 30, 0, 0), 7, 131, 2, 4, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 131);
        }
    }
}
