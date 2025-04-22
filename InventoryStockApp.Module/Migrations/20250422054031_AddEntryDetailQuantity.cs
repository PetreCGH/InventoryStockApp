using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryStockApp.Module.Migrations
{
    /// <inheritdoc />
    public partial class AddEntryDetailQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Partners_PartnerId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Warehouse_WarehouseId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_EntryDetails_Products_ProductId",
                table: "EntryDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExitDetails_Products_ProductId",
                table: "ExitDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Exits_Partners_PartnerId",
                table: "Exits");

            migrationBuilder.DropForeignKey(
                name: "FK_Exits_Warehouse_WarehouseId",
                table: "Exits");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "Exits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PartnerId",
                table: "Exits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Exits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "ExitDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ExitDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ExitId",
                table: "ExitDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "EntryDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "EntryDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EntryId",
                table: "EntryDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "Entries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PartnerId",
                table: "Entries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Partners_PartnerId",
                table: "Entries",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Warehouse_WarehouseId",
                table: "Entries",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EntryDetails_Products_ProductId",
                table: "EntryDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExitDetails_Products_ProductId",
                table: "ExitDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exits_Partners_PartnerId",
                table: "Exits",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exits_Warehouse_WarehouseId",
                table: "Exits",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Partners_PartnerId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Warehouse_WarehouseId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_EntryDetails_Products_ProductId",
                table: "EntryDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExitDetails_Products_ProductId",
                table: "ExitDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Exits_Partners_PartnerId",
                table: "Exits");

            migrationBuilder.DropForeignKey(
                name: "FK_Exits_Warehouse_WarehouseId",
                table: "Exits");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Exits");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "Exits",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PartnerId",
                table: "Exits",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "ExitDetails",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ExitDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExitId",
                table: "ExitDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "EntryDetails",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "EntryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EntryId",
                table: "EntryDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "Entries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PartnerId",
                table: "Entries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Partners_PartnerId",
                table: "Entries",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Warehouse_WarehouseId",
                table: "Entries",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntryDetails_Products_ProductId",
                table: "EntryDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExitDetails_Products_ProductId",
                table: "ExitDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exits_Partners_PartnerId",
                table: "Exits",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exits_Warehouse_WarehouseId",
                table: "Exits",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
