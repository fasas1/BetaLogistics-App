using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetaLogistics.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationUpdates",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipmentId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationUpdates", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_LocationUpdates_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_LocationUpdates_ShipmentId",
                table: "LocationUpdates",
                column: "ShipmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationUpdates");

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 1,
                column: "EstimatedDelivery",
                value: new DateTime(2024, 8, 31, 21, 14, 2, 163, DateTimeKind.Local).AddTicks(5591));

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 2,
                column: "EstimatedDelivery",
                value: new DateTime(2024, 8, 26, 21, 14, 2, 163, DateTimeKind.Local).AddTicks(5622));

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "ShipmentId",
                keyValue: 3,
                column: "EstimatedDelivery",
                value: new DateTime(2024, 8, 30, 21, 14, 2, 163, DateTimeKind.Local).AddTicks(5627));
        }
    }
}
