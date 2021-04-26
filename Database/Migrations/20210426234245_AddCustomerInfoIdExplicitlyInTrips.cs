using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddCustomerInfoIdExplicitlyInTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_CustomerInfo_CustomerInfoId",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerInfoId",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_CustomerInfo_CustomerInfoId",
                table: "Trips",
                column: "CustomerInfoId",
                principalTable: "CustomerInfo",
                principalColumn: "CustomerInfoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_CustomerInfo_CustomerInfoId",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerInfoId",
                table: "Trips",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_CustomerInfo_CustomerInfoId",
                table: "Trips",
                column: "CustomerInfoId",
                principalTable: "CustomerInfo",
                principalColumn: "CustomerInfoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
