using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class PointsClaimed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "pointsClaimed",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pointsClaimed",
                table: "Trips");
        }
    }
}
