using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class MoreSchemaChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CustomerInfo_CustomerInfoId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CustomerInfo_CustomerInfoId1",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_CustomerInfo_CustomerInfoId2",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Trips_TripId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CustomerInfoId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CustomerInfoId1",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CustomerInfoId2",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CustomerInfoId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CustomerInfoId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CustomerInfoId2",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "CustomerInfoId",
                table: "Trips",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Tickets",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CustomerInfoId",
                table: "Trips",
                column: "CustomerInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Trips_TripId",
                table: "Tickets",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_CustomerInfo_CustomerInfoId",
                table: "Trips",
                column: "CustomerInfoId",
                principalTable: "CustomerInfo",
                principalColumn: "CustomerInfoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Trips_TripId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_CustomerInfo_CustomerInfoId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_CustomerInfoId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CustomerInfoId",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "TripId",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerInfoId",
                table: "Tickets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerInfoId1",
                table: "Tickets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerInfoId2",
                table: "Tickets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CustomerInfoId",
                table: "Tickets",
                column: "CustomerInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CustomerInfoId1",
                table: "Tickets",
                column: "CustomerInfoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CustomerInfoId2",
                table: "Tickets",
                column: "CustomerInfoId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CustomerInfo_CustomerInfoId",
                table: "Tickets",
                column: "CustomerInfoId",
                principalTable: "CustomerInfo",
                principalColumn: "CustomerInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CustomerInfo_CustomerInfoId1",
                table: "Tickets",
                column: "CustomerInfoId1",
                principalTable: "CustomerInfo",
                principalColumn: "CustomerInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_CustomerInfo_CustomerInfoId2",
                table: "Tickets",
                column: "CustomerInfoId2",
                principalTable: "CustomerInfo",
                principalColumn: "CustomerInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Trips_TripId",
                table: "Tickets",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "TripId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
