using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BetaLogistics.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "MAxwell,Calabar", "Wunmi Martins", "09043214290" },
                    { 2, "Iyanapaja,Lagos", "Goke Quadri", "080213434370" },
                    { 3, "Ikeja, Lagos", "Madu Chinedu", "08125467893" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "VehicleId", "LicensePlate", "Model" },
                values: new object[,]
                {
                    { 1, "BWC823", "Truck" },
                    { 2, "XYZ789", "Van" },
                    { 3, "DES439", "Hummer Bus" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "DriverId", "LicenseNumber", "Name", "VehicleId" },
                values: new object[,]
                {
                    { 1, "D15445", "Adefemi Adedayo", 3 },
                    { 2, "D67090", "Azeez Babalola", 2 },
                    { 3, "B47630", "Uche Phillips", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "DriverId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "DriverId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "DriverId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "VehicleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "VehicleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "VehicleId",
                keyValue: 3);
        }
    }
}
