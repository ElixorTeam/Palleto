using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Update_Uid1C_Unique_PalletMenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PALLET_MEN_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN");

            migrationBuilder.AlterColumn<Guid>(
                name: "WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PALLET_MEN_UID_1C",
                schema: "REF",
                table: "PALLET_MEN",
                column: "UID_1C",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLET_MEN_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PALLET_MEN_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN");

            migrationBuilder.DropIndex(
                name: "UQ_PALLET_MEN_UID_1C",
                schema: "REF",
                table: "PALLET_MEN");

            migrationBuilder.AlterColumn<Guid>(
                name: "WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_PALLET_MEN_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID");
        }
    }
}
