using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class TestFlightSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "ArrivalTime", "Cost", "DepartureTime", "DestinationAirportId", "FlightNumber", "OriginAirportId", "PlaneTypePlaneId", "TicketsPurchased" },
                values: new object[] { 1, new DateTime(2021, 5, 7, 13, 25, 0, 0, DateTimeKind.Unspecified), 75, new DateTime(2021, 5, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 10, 1, 75 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "FlightId",
                keyValue: 1);
        }
    }
}
