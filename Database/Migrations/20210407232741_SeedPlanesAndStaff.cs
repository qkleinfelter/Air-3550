using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class SeedPlanesAndStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxDistance", "MaxSeats", "Model" },
                values: new object[] { 1, 6570, 230, "Boeing 737 MAX" });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxDistance", "MaxSeats", "Model" },
                values: new object[] { 2, 14815, 416, "Boeing 747" });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxDistance", "MaxSeats", "Model" },
                values: new object[] { 3, 6241, 199, "Boeing 757" });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "PlaneId", "MaxDistance", "MaxSeats", "Model" },
                values: new object[] { 4, 17395, 550, "Boeing 777" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "HashedPass", "LoginId", "UserRole" },
                values: new object[] { 1, "360caebd9edb68609c0933bade3565350e59e284cc503ce61bf0eebd42fb7e5bd657a71ed1498225168757e7f1095920411cce27779e0c778ec52535deae2040", "marketing_manager", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "HashedPass", "LoginId", "UserRole" },
                values: new object[] { 2, "41ec260efa3aa054a91cc6cf9441e3652637f75946c8fa6c2e926f289e095f46e62e19a28e02fb0fc25dd047b1f04e6e03cf930d464b540c40c0045eb6b7e252", "load_engineer", 2 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "HashedPass", "LoginId", "UserRole" },
                values: new object[] { 3, "ab8f196b4521a3aba1de420fe6ef552ce406d8551c3aef370f909cea85abbc77ca1d698ef31f659097eee16e365975047ff403df1aae6cc7ef54595c3ae4d172", "accounting_manager", 3 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "HashedPass", "LoginId", "UserRole" },
                values: new object[] { 4, "e2289f3f3a66a81f5ffb52dff1a09cd2ae91a39eb248230d2907084679c6bacdb4a880da7835ca72003d8c37e107cf91c5f9795678303754eba1be42039bff4d", "flight_manager", 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "PlaneId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "PlaneId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "PlaneId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Planes",
                keyColumn: "PlaneId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);
        }
    }
}
