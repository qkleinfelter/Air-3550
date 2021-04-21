using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class ScheduledFlightsDBSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledFlight_Flights_FlightId",
                table: "ScheduledFlight");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ScheduledFlight_FlightScheduledFlightId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduledFlight",
                table: "ScheduledFlight");

            migrationBuilder.RenameTable(
                name: "ScheduledFlight",
                newName: "ScheduledFlights");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledFlight_FlightId",
                table: "ScheduledFlights",
                newName: "IX_ScheduledFlights_FlightId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduledFlights",
                table: "ScheduledFlights",
                column: "ScheduledFlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledFlights_Flights_FlightId",
                table: "ScheduledFlights",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ScheduledFlights_FlightScheduledFlightId",
                table: "Tickets",
                column: "FlightScheduledFlightId",
                principalTable: "ScheduledFlights",
                principalColumn: "ScheduledFlightId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledFlights_Flights_FlightId",
                table: "ScheduledFlights");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ScheduledFlights_FlightScheduledFlightId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduledFlights",
                table: "ScheduledFlights");

            migrationBuilder.RenameTable(
                name: "ScheduledFlights",
                newName: "ScheduledFlight");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledFlights_FlightId",
                table: "ScheduledFlight",
                newName: "IX_ScheduledFlight_FlightId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduledFlight",
                table: "ScheduledFlight",
                column: "ScheduledFlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledFlight_Flights_FlightId",
                table: "ScheduledFlight",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ScheduledFlight_FlightScheduledFlightId",
                table: "Tickets",
                column: "FlightScheduledFlightId",
                principalTable: "ScheduledFlight",
                principalColumn: "ScheduledFlightId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
