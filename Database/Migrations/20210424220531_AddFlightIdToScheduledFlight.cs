using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddFlightIdToScheduledFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledFlights_Flights_FlightId",
                table: "ScheduledFlights");

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "ScheduledFlights",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledFlights_Flights_FlightId",
                table: "ScheduledFlights",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledFlights_Flights_FlightId",
                table: "ScheduledFlights");

            migrationBuilder.AlterColumn<int>(
                name: "FlightId",
                table: "ScheduledFlights",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledFlights_Flights_FlightId",
                table: "ScheduledFlights",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "FlightId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
