using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class MoreIdsToFixErrors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Airports_DestinationAirportId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Airports_OriginAirportId",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "OriginAirportId",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DestinationAirportId",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Airports_DestinationAirportId",
                table: "Trips",
                column: "DestinationAirportId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Airports_OriginAirportId",
                table: "Trips",
                column: "OriginAirportId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Airports_DestinationAirportId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Airports_OriginAirportId",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "OriginAirportId",
                table: "Trips",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationAirportId",
                table: "Trips",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Airports_DestinationAirportId",
                table: "Trips",
                column: "DestinationAirportId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Airports_OriginAirportId",
                table: "Trips",
                column: "OriginAirportId",
                principalTable: "Airports",
                principalColumn: "AirportId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
