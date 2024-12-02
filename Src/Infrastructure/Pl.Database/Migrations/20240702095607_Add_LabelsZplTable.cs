using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_LabelsZplTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NESTINGS_PLUS_UID",
                schema: "REF_1C",
                table: "NESTINGS");

            migrationBuilder.DropColumn(
                name: "ZPL",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.AddColumn<short>(
                name: "ROTATE",
                schema: "ZPL",
                table: "TEMPLATES",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "LABELS_ZPL",
                schema: "PRINT",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZPL = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    WIDTH = table.Column<short>(type: "smallint", nullable: false),
                    HEIGHT = table.Column<short>(type: "smallint", nullable: false),
                    ROTATE = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LABELS_ZPL", x => x.UID);
                    table.ForeignKey(
                        name: "FK_LABELS_ZPL__LABEL",
                        column: x => x.UID,
                        principalSchema: "PRINT",
                        principalTable: "LABELS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_NESTINGS__PLU",
                schema: "REF_1C",
                table: "NESTINGS",
                column: "UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NESTINGS__PLU",
                schema: "REF_1C",
                table: "NESTINGS");

            migrationBuilder.DropTable(
                name: "LABELS_ZPL",
                schema: "PRINT");

            migrationBuilder.DropColumn(
                name: "ROTATE",
                schema: "ZPL",
                table: "TEMPLATES");

            migrationBuilder.AddColumn<string>(
                name: "ZPL",
                schema: "PRINT",
                table: "LABELS",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_NESTINGS_PLUS_UID",
                schema: "REF_1C",
                table: "NESTINGS",
                column: "UID",
                principalSchema: "REF_1C",
                principalTable: "PLUS",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
