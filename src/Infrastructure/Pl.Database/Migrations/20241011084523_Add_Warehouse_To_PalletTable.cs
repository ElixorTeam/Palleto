using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Warehouse_To_PalletTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS__ARM",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.AddColumn<Guid>(
                name: "WAREHOUSE_UID",
                schema: "PRINT",
                table: "PALLETS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PALLETS_WAREHOUSE_UID",
                schema: "PRINT",
                table: "PALLETS",
                column: "WAREHOUSE_UID");

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS__ARM",
                schema: "PRINT",
                table: "PALLETS",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(@"
                UPDATE p
                SET WAREHOUSE_UID = man.WAREHOUSE_UID
                FROM [PRINT].[PALLETS] AS p
                JOIN [REF].[PALLET_MEN] AS man ON p.PALLET_MAN_UID = man.UID
            ");

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS__WAREHOUSE",
                schema: "PRINT",
                table: "PALLETS",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS__ARM",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS__WAREHOUSE",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropIndex(
                name: "IX_PALLETS_WAREHOUSE_UID",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropColumn(
                name: "WAREHOUSE_UID",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS__ARM",
                schema: "PRINT",
                table: "PALLETS",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
