using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class CancellationAndCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCanceled",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "totalCost",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isCanceled",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCanceled",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "totalCost",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "isCanceled",
                table: "Tickets");
        }
    }
}
