using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetaLogistics.Migrations
{
    /// <inheritdoc />
    public partial class SeedHotelToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 1,
                column: "EstimatedDelivery",
                value: new DateTime(2024, 9, 12, 10, 34, 53, 936, DateTimeKind.Local).AddTicks(4447));

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 2,
                column: "EstimatedDelivery",
                value: new DateTime(2024, 9, 7, 10, 34, 53, 936, DateTimeKind.Local).AddTicks(4470));

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 3,
                column: "EstimatedDelivery",
                value: new DateTime(2024, 9, 11, 10, 34, 53, 936, DateTimeKind.Local).AddTicks(4473));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 1,
                column: "EstimatedDelivery",
                value: new DateTime(2024, 9, 5, 12, 22, 13, 657, DateTimeKind.Local).AddTicks(5412));

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 2,
                column: "EstimatedDelivery",
                value: new DateTime(2024, 8, 31, 12, 22, 13, 657, DateTimeKind.Local).AddTicks(5436));

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 3,
                column: "EstimatedDelivery",
                value: new DateTime(2024, 9, 4, 12, 22, 13, 657, DateTimeKind.Local).AddTicks(5439));
        }
    }
}
