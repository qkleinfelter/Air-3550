using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class CustInfoToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustInfoCustomerInfoId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentType",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

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

            migrationBuilder.CreateTable(
                name: "CustomerInfo",
                columns: table => new
                {
                    CustomerInfoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    Zip = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreditCardNumber = table.Column<string>(type: "TEXT", nullable: false),
                    PointsUsed = table.Column<int>(type: "INTEGER", nullable: false),
                    PointsAvailable = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditBalance = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfo", x => x.CustomerInfoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustInfoCustomerInfoId",
                table: "Users",
                column: "CustInfoCustomerInfoId");

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
                name: "FK_Users_CustomerInfo_CustInfoCustomerInfoId",
                table: "Users",
                column: "CustInfoCustomerInfoId",
                principalTable: "CustomerInfo",
                principalColumn: "CustomerInfoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Users_CustomerInfo_CustInfoCustomerInfoId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CustomerInfo");

            migrationBuilder.DropIndex(
                name: "IX_Users_CustInfoCustomerInfoId",
                table: "Users");

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
                name: "CustInfoCustomerInfoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CustomerInfoId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CustomerInfoId1",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "CustomerInfoId2",
                table: "Tickets");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "Tickets",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
