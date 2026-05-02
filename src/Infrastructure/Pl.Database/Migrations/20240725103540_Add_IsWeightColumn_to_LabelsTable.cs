using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsWeightColumn_to_LabelsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_PRODUCTION_SITES__ADDRESS",
                schema: "REF",
                table: "PRODUCTION_SITES");

            migrationBuilder.AddColumn<bool>(
                name: "IS_WEIGHT",
                schema: "PRINT",
                table: "LABELS",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IS_WEIGHT",
                schema: "PRINT",
                table: "LABELS");

            migrationBuilder.CreateIndex(
                name: "UQ_PRODUCTION_SITES__ADDRESS",
                schema: "REF",
                table: "PRODUCTION_SITES",
                column: "ADDRESS",
                unique: true);
        }
    }
}
