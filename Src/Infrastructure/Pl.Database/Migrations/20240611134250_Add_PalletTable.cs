using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_PalletTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PALLET_UID",
                schema: "PRINT",
                table: "LABELS",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PALLETS",
                schema: "PRINT",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PLU_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ARM_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PALLET_MAN_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NUMBER = table.Column<long>(type: "bigint", nullable: false),
                    COUNTER = table.Column<long>(type: "bigint", nullable: false),
                    PRODUCT_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TRAY_WEIGHT = table.Column<decimal>(type: "decimal(5,3)", nullable: false),
                    BARCODE = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PALLETS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_PALLETS_ARMS_ARM_UID",
                        column: x => x.ARM_UID,
                        principalSchema: "REF",
                        principalTable: "ARMS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PALLETS_PALLET_MEN_PALLET_MAN_UID",
                        column: x => x.PALLET_MAN_UID,
                        principalSchema: "REF",
                        principalTable: "PALLET_MEN",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PALLETS_PLUS_PLU_UID",
                        column: x => x.PLU_UID,
                        principalSchema: "REF_1C",
                        principalTable: "PLUS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PALLETS_ARM_UID",
                schema: "PRINT",
                table: "PALLETS",
                column: "ARM_UID");

            migrationBuilder.CreateIndex(
                name: "IX_PALLETS_PALLET_MAN_UID",
                schema: "PRINT",
                table: "PALLETS",
                column: "PALLET_MAN_UID");

            migrationBuilder.CreateIndex(
                name: "IX_PALLETS_PLU_UID",
                schema: "PRINT",
                table: "PALLETS",
                column: "PLU_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_PALLETS_BARCODE",
                schema: "PRINT",
                table: "PALLETS",
                column: "BARCODE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PALLETS_COUNTER",
                schema: "PRINT",
                table: "PALLETS",
                column: "COUNTER",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PALLETS",
                schema: "PRINT");

            migrationBuilder.DropColumn(
                name: "PALLET_UID",
                schema: "PRINT",
                table: "LABELS");
        }
    }
}
