using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_TypeColumn_to_ZplResourcesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TYPE",
                schema: "ZPL",
                table: "RESOURCES",
                type: "varchar(4)",
                nullable: false,
                defaultValue: "Text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TYPE",
                schema: "ZPL",
                table: "RESOURCES");
        }
    }
}
