using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryStockApp.Module.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ID",
                keyValue: new Guid("108d9487-846a-4fd3-9c83-c1cdfb72aa04"),
                column: "DisplayName",
                value: "Entry Report");

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ID",
                keyValue: new Guid("108d94d7-846a-4fd3-9c83-c1cdfb72aa04"),
                column: "DisplayName",
                value: "Exit Report");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ID",
                keyValue: new Guid("108d9487-846a-4fd3-9c83-c1cdfb72aa04"),
                column: "DisplayName",
                value: "Raport intrari");

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "ID",
                keyValue: new Guid("108d94d7-846a-4fd3-9c83-c1cdfb72aa04"),
                column: "DisplayName",
                value: "Raport iesiri");
        }
    }
}
