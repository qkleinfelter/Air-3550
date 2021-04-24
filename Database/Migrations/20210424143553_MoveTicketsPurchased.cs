using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class MoveTicketsPurchased : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketsPurchased",
                table: "Flights");

            migrationBuilder.AddColumn<int>(
                name: "TicketsPurchased",
                table: "ScheduledFlights",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketsPurchased",
                table: "ScheduledFlights");

            migrationBuilder.AddColumn<int>(
                name: "TicketsPurchased",
                table: "Flights",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
