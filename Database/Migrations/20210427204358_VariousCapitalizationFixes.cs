using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class VariousCapitalizationFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "totalCost",
                table: "Trips",
                newName: "TotalCost");

            migrationBuilder.RenameColumn(
                name: "pointsClaimed",
                table: "Trips",
                newName: "PointsClaimed");

            migrationBuilder.RenameColumn(
                name: "isCanceled",
                table: "Trips",
                newName: "IsCanceled");

            migrationBuilder.RenameColumn(
                name: "isCanceled",
                table: "Tickets",
                newName: "IsCanceled");

            migrationBuilder.RenameColumn(
                name: "isCanceled",
                table: "Flights",
                newName: "IsCanceled");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalCost",
                table: "Trips",
                newName: "totalCost");

            migrationBuilder.RenameColumn(
                name: "PointsClaimed",
                table: "Trips",
                newName: "pointsClaimed");

            migrationBuilder.RenameColumn(
                name: "IsCanceled",
                table: "Trips",
                newName: "isCanceled");

            migrationBuilder.RenameColumn(
                name: "IsCanceled",
                table: "Tickets",
                newName: "isCanceled");

            migrationBuilder.RenameColumn(
                name: "IsCanceled",
                table: "Flights",
                newName: "isCanceled");
        }
    }
}
