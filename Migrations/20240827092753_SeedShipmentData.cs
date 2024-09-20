using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BetaLogistics.Migrations
{
    /// <inheritdoc />
    public partial class SeedShipmentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shipments",
                columns: new[] { "ShipmentId", "CustomerId", "Destination", "DriverId", "EstimatedDelivery", "Origin", "Status", "TrackingNumber", "VehicleId" },
                values: new object[,]
                {
                    { 1, 1, "Port Harcourt", null, new DateTime(2024, 8, 30, 2, 27, 52, 611, DateTimeKind.Local).AddTicks(9580), "Lagos", "In Transit", "TRK123456", 1 },
                    { 2, 2, "Lagos", null, new DateTime(2024, 8, 25, 2, 27, 52, 611, DateTimeKind.Local).AddTicks(9647), "Calabar", "Delivered", "TRK654321", 2 },
                    { 3, 3, "Abuja", null, new DateTime(2024, 8, 29, 2, 27, 52, 611, DateTimeKind.Local).AddTicks(9651), "Lagos", "In Transit", "TRK654321", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 3);
        }
    }
}
