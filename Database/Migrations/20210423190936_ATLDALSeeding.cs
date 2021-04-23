using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class ATLDALSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased", "isCanceled" },
                values: new object[] { 113, new TimeSpan(0, 7, 45, 0, 0), 5, 113, 2, 2, 0, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased", "isCanceled" },
                values: new object[] { 114, new TimeSpan(0, 9, 20, 0, 0), 5, 114, 2, 2, 0, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased", "isCanceled" },
                values: new object[] { 115, new TimeSpan(0, 13, 10, 0, 0), 5, 115, 2, 2, 0, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased", "isCanceled" },
                values: new object[] { 116, new TimeSpan(0, 17, 20, 0, 0), 5, 116, 2, 2, 0, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased", "isCanceled" },
                values: new object[] { 117, new TimeSpan(0, 8, 10, 0, 0), 2, 117, 5, 2, 0, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased", "isCanceled" },
                values: new object[] { 118, new TimeSpan(0, 10, 15, 0, 0), 2, 118, 5, 2, 0, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased", "isCanceled" },
                values: new object[] { 119, new TimeSpan(0, 14, 45, 0, 0), 2, 119, 5, 2, 0, false });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased", "isCanceled" },
                values: new object[] { 120, new TimeSpan(0, 18, 20, 0, 0), 2, 120, 5, 2, 0, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 120);
        }
    }
}
