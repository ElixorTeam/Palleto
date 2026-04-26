using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_BarcodesBodyColumns_to_TemplatesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BARCODE_BOTTOM_BODY",
                schema: "ZPL",
                table: "TEMPLATES",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BARCODE_RIGHT_BODY",
                schema: "ZPL",
                table: "TEMPLATES",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BARCODE_TOP_BODY",
                schema: "ZPL",
                table: "TEMPLATES",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BARCODE_BOTTOM_BODY",
                schema: "ZPL",
                table: "TEMPLATES");

            migrationBuilder.DropColumn(
                name: "BARCODE_RIGHT_BODY",
                schema: "ZPL",
                table: "TEMPLATES");

            migrationBuilder.DropColumn(
                name: "BARCODE_TOP_BODY",
                schema: "ZPL",
                table: "TEMPLATES");
        }
    }
}
