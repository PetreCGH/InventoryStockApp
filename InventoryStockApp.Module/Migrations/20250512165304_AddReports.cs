using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryStockApp.Module.Migrations
{
    /// <inheritdoc />
    public partial class AddReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportSelectionParameters");

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ID", "Content", "DataTypeName", "DisplayName", "IsInplaceReport", "ParametersObjectTypeName", "PredefinedReportTypeName" },
                values: new object[,]
                {
                    { new Guid("108d9487-846a-4fd3-9c83-c1cdfb72aa04"), null, "", "Raport intrari", false, "InventoryStockApp.Module.Reports.ReportSelectionParameters", "Raport intrari" },
                    { new Guid("108d94d7-846a-4fd3-9c83-c1cdfb72aa04"), null, "", "Raport iesiri", false, "InventoryStockApp.Module.Reports.ReportSelectionParameters", "Raport iesiri" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ID",
                keyValue: new Guid("108d9487-846a-4fd3-9c83-c1cdfb72aa04"));

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "ID",
                keyValue: new Guid("108d94d7-846a-4fd3-9c83-c1cdfb72aa04"));

            migrationBuilder.CreateTable(
                name: "ReportSelectionParameters",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    AllWarehouses = table.Column<bool>(type: "bit", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ReportType = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportSelectionParameters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReportSelectionParameters_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportSelectionParameters_WarehouseId",
                table: "ReportSelectionParameters",
                column: "WarehouseId");
        }
    }
}
