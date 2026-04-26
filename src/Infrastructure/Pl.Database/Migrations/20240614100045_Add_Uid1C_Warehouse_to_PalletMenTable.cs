using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Uid1C_Warehouse_to_PalletMenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UID_1C",
                schema: "REF",
                table: "PALLET_MEN",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PALLET_MEN_WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN",
                column: "WAREHOUSE_UID");

            migrationBuilder.AddForeignKey(
                name: "FK_PALLET_MEN_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PALLET_MEN_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN");

            migrationBuilder.DropIndex(
                name: "IX_PALLET_MEN_WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN");

            migrationBuilder.DropColumn(
                name: "UID_1C",
                schema: "REF",
                table: "PALLET_MEN");

            migrationBuilder.DropColumn(
                name: "WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN");
        }
    }
}
