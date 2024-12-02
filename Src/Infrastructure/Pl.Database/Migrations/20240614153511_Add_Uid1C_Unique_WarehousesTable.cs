using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Uid1C_Unique_WarehousesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UQ_WAREHOUSES_UID_1C",
                schema: "REF",
                table: "WAREHOUSES",
                column: "UID_1C",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_WAREHOUSES_UID_1C",
                schema: "REF",
                table: "WAREHOUSES");
        }
    }
}
