using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryStockApp.Module.Migrations
{
    /// <inheritdoc />
    public partial class AddReportsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ID",
                keyValue: new Guid("108d9487-846a-4fd3-9c83-c1cdfb72aa04"),
                column: "PredefinedReportTypeName",
                value: "InventoryStockApp.Module.Reports.EntryReport");

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ID",
                keyValue: new Guid("108d94d7-846a-4fd3-9c83-c1cdfb72aa04"),
                column: "PredefinedReportTypeName",
                value: "InventoryStockApp.Module.Reports.ExitReport");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ID",
                keyValue: new Guid("108d9487-846a-4fd3-9c83-c1cdfb72aa04"),
                column: "PredefinedReportTypeName",
                value: "Raport intrari");

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ID",
                keyValue: new Guid("108d94d7-846a-4fd3-9c83-c1cdfb72aa04"),
                column: "PredefinedReportTypeName",
                value: "Raport iesiri");
        }
    }
}
