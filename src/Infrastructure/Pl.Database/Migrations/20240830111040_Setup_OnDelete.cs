using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Setup_OnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CHARACTERISTICS__BOX",
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

            migrationBuilder.RenameIndex(
                name: "UQ_PALLET_MEN__IP",
                schema: "REF",
                table: "PRINTERS",
                newName: "UQ_PRINTERS__IP");

            migrationBuilder.AddForeignKey(
                name: "FK_CHARACTERISTICS__BOX",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                column: "BOX_UID",
                principalSchema: "REF_1C",
                principalTable: "BOXES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS__ARM",
                schema: "PRINT",
                table: "LABELS",
                column: "ARM_UID",
                principalSchema: "REF",
                principalTable: "ARMS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS__PALLET",
                schema: "PRINT",
                table: "LABELS",
                column: "PALLET_UID",
                principalSchema: "PRINT",
                principalTable: "PALLETS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LABELS__PLU",
                schema: "PRINT",
                table: "LABELS",
                column: "PLU_UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_NESTINGS__BOX",
                schema: "REF_1C",
                table: "NESTINGS",
                column: "BOX_UID",
                principalSchema: "REF_1C",
                principalTable: "BOXES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLET_MEN__WAREHOUSE",
                schema: "REF",
                table: "PALLET_MEN",
                column: "WAREHOUSE_UID",
                principalSchema: "REF",
                principalTable: "WAREHOUSES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PALLETS__PALLET_MAN",
                schema: "PRINT",
                table: "PALLETS",
                column: "PALLET_MAN_UID",
                principalSchema: "REF",
                principalTable: "PALLET_MEN",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__BRAND",
                schema: "REF_1C",
                table: "PLUS",
                column: "BRAND_UID",
                principalSchema: "REF_1C",
                principalTable: "BRANDS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__BUNDLE",
                schema: "REF_1C",
                table: "PLUS",
                column: "BUNDLE_UID",
                principalSchema: "REF_1C",
                principalTable: "BUNDLES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__CLIP",
                schema: "REF_1C",
                table: "PLUS",
                column: "CLIP_UID",
                principalSchema: "REF_1C",
                principalTable: "CLIPS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PLUS__TEMPLATE",
                schema: "REF_1C",
                table: "PLUS",
                column: "TEMPLATE_UID",
                principalSchema: "ZPL",
                principalTable: "TEMPLATES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PRINTERS__PRODUCTION_SITE",
                schema: "REF",
                table: "PRINTERS",
                column: "PRODUCTION_SITE_UID",
                principalSchema: "REF",
                principalTable: "PRODUCTION_SITES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WAREHOUSES__PRODUCTION_SITE",
                schema: "REF",
                table: "WAREHOUSES",
                column: "PRODUCTION_SITE_UID",
                principalSchema: "REF",
                principalTable: "PRODUCTION_SITES",
                principalColumn: "UID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CHARACTERISTICS__BOX",
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
                name: "FK_PRINTERS__PRODUCTION_SITE",
                schema: "REF",
                table: "PRINTERS");

            migrationBuilder.DropForeignKey(
                name: "FK_WAREHOUSES__PRODUCTION_SITE",
                schema: "REF",
                table: "WAREHOUSES");

            migrationBuilder.RenameIndex(
                name: "UQ_PRINTERS__IP",
                schema: "REF",
                table: "PRINTERS",
                newName: "UQ_PALLET_MEN__IP");

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
    }
}
