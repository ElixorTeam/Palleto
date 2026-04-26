using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Rename_all_indexes_fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_PRINTERS_PRINTER_UID",
                schema: "REF",
                table: "ARMS");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "ARMS");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_PLUS_FK_ARMS_ARM_UID",
                table: "ARMS_PLUS_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_PLUS_FK_PLUS_PLU_UID",
                table: "ARMS_PLUS_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_CHARACTERISTICS_BOXES_BOX_UID",
                schema: "REF_1C",
                table: "CHARACTERISTICS");

            migrationBuilder.DropForeignKey(
                name: "FK_CHARACTERISTICS_PLUS_PLU_UID",
                schema: "REF_1C",
                table: "CHARACTERISTICS");

            migrationBuilder.DropForeignKey(
                name: "FK_LABELS_ARMS_ARM_UID",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.DropForeignKey(
                name: "FK_LABELS_PLUS_PLU_UID",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.DropForeignKey(
                name: "FK_NESTINGS_BOXES_BOX_UID",
                schema: "REF_1C",
                table: "NESTINGS");

            migrationBuilder.DropForeignKey(
                name: "FK_PALLET_MEN_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN");

            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS_ARMS_ARM_UID",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS_PALLET_MEN_PALLET_MAN_UID",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS_PLUS_PLU_UID",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_BRANDS_BRAND_UID",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_BUNDLES_BUNDLE_UID",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS_CLIPS_CLIP_UID",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRINTERS_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "PRINTERS");

            migrationBuilder.DropForeignKey(
                name: "FK_WAREHOUSES_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "WAREHOUSES");

            migrationBuilder.DropIndex(
                name: "UQ_STORAGE_METHODS_ZPL",
                schema: "ZPL",
                table: "STORAGE_METHODS");

            migrationBuilder.DropIndex(
                name: "IX_PALLETS_PLU_UID",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropColumn(
                name: "PLU_UID",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.RenameIndex(
                name: "UQ_WAREHOUSES_UID_1C",
                schema: "REF",
                table: "WAREHOUSES",
                newName: "UQ_WAREHOUSES__UID_1C");

            migrationBuilder.RenameIndex(
                name: "UQ_WAREHOUSES_NAME",
                schema: "REF",
                table: "WAREHOUSES",
                newName: "UQ_WAREHOUSES__NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_TEMPLATES_NAME_IS_WEIGHT",
                schema: "ZPL",
                table: "TEMPLATES",
                newName: "UQ_TEMPLATES__NAME__IS_WEIGHT");

            migrationBuilder.RenameIndex(
                name: "UQ_STORAGE_METHODS_NAME",
                schema: "ZPL",
                table: "STORAGE_METHODS",
                newName: "UQ_STORAGE_METHODS__NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_RESOURCES_NAME",
                schema: "ZPL",
                table: "RESOURCES",
                newName: "UQ_RESOURCES__NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_PRODUCTION_SITES_NAME",
                schema: "REF",
                table: "PRODUCTION_SITES",
                newName: "UQ_PRODUCTION_SITES__NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_PRODUCTION_SITES_ADDRESS",
                schema: "REF",
                table: "PRODUCTION_SITES",
                newName: "UQ_PRODUCTION_SITES__ADDRESS");

            migrationBuilder.RenameColumn(
                name: "IP_V4",
                schema: "REF",
                table: "PRINTERS",
                newName: "IP");

            migrationBuilder.RenameIndex(
                name: "UQ_PRINTERS_NAME",
                schema: "REF",
                table: "PRINTERS",
                newName: "UQ_PRINTERS__NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_PRINTERS_IP_V4",
                schema: "REF",
                table: "PRINTERS",
                newName: "UQ_PALLET_MEN__IP");

            migrationBuilder.RenameIndex(
                name: "UQ_PLUS_NUMBER",
                schema: "REF_1C",
                table: "PLUS",
                newName: "UQ_PLUS__NUMBER");

            migrationBuilder.RenameColumn(
                name: "TRAY_WEIGHT",
                schema: "PRINT",
                table: "PALLETS",
                newName: "WEIGHT_TRAY");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLETS_COUNTER",
                schema: "PRINT",
                table: "PALLETS",
                newName: "UQ_PALLETS__COUNTER");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLETS_BARCODE",
                schema: "PRINT",
                table: "PALLETS",
                newName: "UQ_PALLETS__BARCODE");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLET_MEN_UID_1C",
                schema: "REF",
                table: "PALLET_MEN",
                newName: "UQ_PALLET_MEN__UID_1C");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLET_MEN_FIO",
                schema: "REF",
                table: "PALLET_MEN",
                newName: "UQ_PALLET_MEN__FIO");

            migrationBuilder.RenameIndex(
                name: "UQ_PLUS_BARCODE_TOP",
                schema: "PRINT",
                table: "LABELS",
                newName: "UQ_LABELS__BARCODE_TOP");

            migrationBuilder.RenameIndex(
                name: "UQ_CHARACTERISTICS_UNIQ",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                newName: "UQ_CHARACTERISTICS__UNIQ");

            migrationBuilder.RenameIndex(
                name: "UQ_BRANDS_NAME",
                schema: "REF_1C",
                table: "BRANDS",
                newName: "UQ_BRANDS__NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS_PC_NAME",
                schema: "REF",
                table: "ARMS",
                newName: "UQ_ARMS__PC_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS_NUMBER",
                schema: "REF",
                table: "ARMS",
                newName: "UQ_ARMS__NUMBER");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS_NAME",
                schema: "REF",
                table: "ARMS",
                newName: "UQ_ARMS__NAME");

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_TEMPLATE_UID",
                schema: "REF_1C",
                table: "PLUS",
                column: "TEMPLATE_UID");

            migrationBuilder.CreateIndex(
                name: "IX_LABELS_PALLET_UID",
                schema: "PRINT",
                table: "LABELS",
                column: "PALLET_UID");

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS__PRINTER",
                schema: "REF",
                table: "ARMS",
                column: "PRINTER_UID",
                principalSchema: "REF",
                principalTable: "PRINTERS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS__WAREHOUSE",
                schema: "REF",
                table: "ARMS",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_PLUS__ARM",
                table: "ARMS_PLUS_FK",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_PLUS__PLU",
                table: "ARMS_PLUS_FK",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CHARACTERISTICS__BOX",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                column: "BOX_UID",
                principalSchema: "REF_1C",
                principalTable: "BOXES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CHARACTERISTICS__PLU",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS__ARM",
                schema: "PRINT",
                table: "LABELS",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS__PALLET",
                schema: "PRINT",
                table: "LABELS",
                column: "PALLET_UID",
                principalSchema: "PRINT",
                principalTable: "PALLETS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS__PLU",
                schema: "PRINT",
                table: "LABELS",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NESTINGS__BOX",
                schema: "REF_1C",
                table: "NESTINGS",
                column: "BOX_UID",
                principalSchema: "REF_1C",
                principalTable: "BOXES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLET_MEN__WAREHOUSE",
                schema: "REF",
                table: "PALLET_MEN",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS__ARM",
                schema: "PRINT",
                table: "PALLETS",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS__PALLET_MAN",
                schema: "PRINT",
                table: "PALLETS",
                column: "PALLET_MAN_UID",
                principalSchema: "REF",
                principalTable: "PALLET_MEN",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__BRAND",
                schema: "REF_1C",
                table: "PLUS",
                column: "BRAND_UID",
                principalSchema: "REF_1C",
                principalTable: "BRANDS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__BUNDLE",
                schema: "REF_1C",
                table: "PLUS",
                column: "BUNDLE_UID",
                principalSchema: "REF_1C",
                principalTable: "BUNDLES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__CLIP",
                schema: "REF_1C",
                table: "PLUS",
                column: "CLIP_UID",
                principalSchema: "REF_1C",
                principalTable: "CLIPS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__TEMPLATE",
                schema: "REF_1C",
                table: "PLUS",
                column: "TEMPLATE_UID",
                principalSchema: "ZPL",
                principalTable: "TEMPLATES",
                principalColumn: "UID");

            migrationBuilder.AddForeignKey(
                name: "FK_PALLET_MEN__PRODUCTION_SITE",
                schema: "REF",
                table: "PRINTERS",
                column: "PRODUCTION_SITE_UID",
                principalSchema: "REF",
                principalTable: "PRODUCTION_SITES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WAREHOUSES__PRODUCTION_SITE",
                schema: "REF",
                table: "WAREHOUSES",
                column: "PRODUCTION_SITE_UID",
                principalSchema: "REF",
                principalTable: "PRODUCTION_SITES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ARMS__PRINTER",
                schema: "REF",
                table: "ARMS");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS__WAREHOUSE",
                schema: "REF",
                table: "ARMS");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_PLUS__ARM",
                table: "ARMS_PLUS_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_ARMS_PLUS__PLU",
                table: "ARMS_PLUS_FK");

            migrationBuilder.DropForeignKey(
                name: "FK_CHARACTERISTICS__BOX",
                schema: "REF_1C",
                table: "CHARACTERISTICS");

            migrationBuilder.DropForeignKey(
                name: "FK_CHARACTERISTICS__PLU",
                schema: "REF_1C",
                table: "CHARACTERISTICS");

            migrationBuilder.DropForeignKey(
                name: "FK_LABELS__ARM",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.DropForeignKey(
                name: "FK_LABELS__PALLET",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.DropForeignKey(
                name: "FK_LABELS__PLU",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.DropForeignKey(
                name: "FK_NESTINGS__BOX",
                schema: "REF_1C",
                table: "NESTINGS");

            migrationBuilder.DropForeignKey(
                name: "FK_PALLET_MEN__WAREHOUSE",
                schema: "REF",
                table: "PALLET_MEN");

            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS__ARM",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropForeignKey(
                name: "FK_PALLETS__PALLET_MAN",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS__BRAND",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS__BUNDLE",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS__CLIP",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropForeignKey(
                name: "FK_PLUS__TEMPLATE",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropForeignKey(
                name: "FK_PALLET_MEN__PRODUCTION_SITE",
                schema: "REF",
                table: "PRINTERS");

            migrationBuilder.DropForeignKey(
                name: "FK_WAREHOUSES__PRODUCTION_SITE",
                schema: "REF",
                table: "WAREHOUSES");

            migrationBuilder.DropIndex(
                name: "IX_PLUS_TEMPLATE_UID",
                schema: "REF_1C",
                table: "PLUS");

            migrationBuilder.DropIndex(
                name: "IX_LABELS_PALLET_UID",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.RenameIndex(
                name: "UQ_WAREHOUSES__UID_1C",
                schema: "REF",
                table: "WAREHOUSES",
                newName: "UQ_WAREHOUSES_UID_1C");

            migrationBuilder.RenameIndex(
                name: "UQ_WAREHOUSES__NAME",
                schema: "REF",
                table: "WAREHOUSES",
                newName: "UQ_WAREHOUSES_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_TEMPLATES__NAME__IS_WEIGHT",
                schema: "ZPL",
                table: "TEMPLATES",
                newName: "UQ_TEMPLATES_NAME_IS_WEIGHT");

            migrationBuilder.RenameIndex(
                name: "UQ_STORAGE_METHODS__NAME",
                schema: "ZPL",
                table: "STORAGE_METHODS",
                newName: "UQ_STORAGE_METHODS_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_RESOURCES__NAME",
                schema: "ZPL",
                table: "RESOURCES",
                newName: "UQ_RESOURCES_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_PRODUCTION_SITES__NAME",
                schema: "REF",
                table: "PRODUCTION_SITES",
                newName: "UQ_PRODUCTION_SITES_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_PRODUCTION_SITES__ADDRESS",
                schema: "REF",
                table: "PRODUCTION_SITES",
                newName: "UQ_PRODUCTION_SITES_ADDRESS");

            migrationBuilder.RenameColumn(
                name: "IP",
                schema: "REF",
                table: "PRINTERS",
                newName: "IP_V4");

            migrationBuilder.RenameIndex(
                name: "UQ_PRINTERS__NAME",
                schema: "REF",
                table: "PRINTERS",
                newName: "UQ_PRINTERS_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLET_MEN__IP",
                schema: "REF",
                table: "PRINTERS",
                newName: "UQ_PRINTERS_IP_V4");

            migrationBuilder.RenameIndex(
                name: "UQ_PLUS__NUMBER",
                schema: "REF_1C",
                table: "PLUS",
                newName: "UQ_PLUS_NUMBER");

            migrationBuilder.RenameColumn(
                name: "WEIGHT_TRAY",
                schema: "PRINT",
                table: "PALLETS",
                newName: "TRAY_WEIGHT");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLETS__COUNTER",
                schema: "PRINT",
                table: "PALLETS",
                newName: "UQ_PALLETS_COUNTER");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLETS__BARCODE",
                schema: "PRINT",
                table: "PALLETS",
                newName: "UQ_PALLETS_BARCODE");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLET_MEN__UID_1C",
                schema: "REF",
                table: "PALLET_MEN",
                newName: "UQ_PALLET_MEN_UID_1C");

            migrationBuilder.RenameIndex(
                name: "UQ_PALLET_MEN__FIO",
                schema: "REF",
                table: "PALLET_MEN",
                newName: "UQ_PALLET_MEN_FIO");

            migrationBuilder.RenameIndex(
                name: "UQ_LABELS__BARCODE_TOP",
                schema: "PRINT",
                table: "LABELS",
                newName: "UQ_PLUS_BARCODE_TOP");

            migrationBuilder.RenameIndex(
                name: "UQ_CHARACTERISTICS__UNIQ",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                newName: "UQ_CHARACTERISTICS_UNIQ");

            migrationBuilder.RenameIndex(
                name: "UQ_BRANDS__NAME",
                schema: "REF_1C",
                table: "BRANDS",
                newName: "UQ_BRANDS_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS__PC_NAME",
                schema: "REF",
                table: "ARMS",
                newName: "UQ_ARMS_PC_NAME");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS__NUMBER",
                schema: "REF",
                table: "ARMS",
                newName: "UQ_ARMS_NUMBER");

            migrationBuilder.RenameIndex(
                name: "UQ_ARMS__NAME",
                schema: "REF",
                table: "ARMS",
                newName: "UQ_ARMS_NAME");

            migrationBuilder.AddColumn<Guid>(
                name: "PLU_UID",
                schema: "PRINT",
                table: "PALLETS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "UQ_STORAGE_METHODS_ZPL",
                schema: "ZPL",
                table: "STORAGE_METHODS",
                column: "ZPL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PALLETS_PLU_UID",
                schema: "PRINT",
                table: "PALLETS",
                column: "PLU_UID");

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_PRINTERS_PRINTER_UID",
                schema: "REF",
                table: "ARMS",
                column: "PRINTER_UID",
                principalSchema: "REF",
                principalTable: "PRINTERS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "ARMS",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_PLUS_FK_ARMS_ARM_UID",
                table: "ARMS_PLUS_FK",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ARMS_PLUS_FK_PLUS_PLU_UID",
                table: "ARMS_PLUS_FK",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CHARACTERISTICS_BOXES_BOX_UID",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                column: "BOX_UID",
                principalSchema: "REF_1C",
                principalTable: "BOXES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CHARACTERISTICS_PLUS_PLU_UID",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS_ARMS_ARM_UID",
                schema: "PRINT",
                table: "LABELS",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS_PLUS_PLU_UID",
                schema: "PRINT",
                table: "LABELS",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID");

            migrationBuilder.AddForeignKey(
                name: "FK_NESTINGS_BOXES_BOX_UID",
                schema: "REF_1C",
                table: "NESTINGS",
                column: "BOX_UID",
                principalSchema: "REF_1C",
                principalTable: "BOXES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLET_MEN_WAREHOUSES_WAREHOUSE_UID",
                schema: "REF",
                table: "PALLET_MEN",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS_ARMS_ARM_UID",
                schema: "PRINT",
                table: "PALLETS",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS_PALLET_MEN_PALLET_MAN_UID",
                schema: "PRINT",
                table: "PALLETS",
                column: "PALLET_MAN_UID",
                principalSchema: "REF",
                principalTable: "PALLET_MEN",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS_PLUS_PLU_UID",
                schema: "PRINT",
                table: "PALLETS",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_BRANDS_BRAND_UID",
                schema: "REF_1C",
                table: "PLUS",
                column: "BRAND_UID",
                principalSchema: "REF_1C",
                principalTable: "BRANDS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_BUNDLES_BUNDLE_UID",
                schema: "REF_1C",
                table: "PLUS",
                column: "BUNDLE_UID",
                principalSchema: "REF_1C",
                principalTable: "BUNDLES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS_CLIPS_CLIP_UID",
                schema: "REF_1C",
                table: "PLUS",
                column: "CLIP_UID",
                principalSchema: "REF_1C",
                principalTable: "CLIPS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRINTERS_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "PRINTERS",
                column: "PRODUCTION_SITE_UID",
                principalSchema: "REF",
                principalTable: "PRODUCTION_SITES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WAREHOUSES_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "WAREHOUSES",
                column: "PRODUCTION_SITE_UID",
                principalSchema: "REF",
                principalTable: "PRODUCTION_SITES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
