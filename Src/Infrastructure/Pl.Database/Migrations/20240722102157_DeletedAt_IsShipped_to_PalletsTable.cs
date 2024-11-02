using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class DeletedAt_IsShipped_to_PalletsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BODY",
                schema: "ZPL",
                table: "TEMPLATES",
                type: "varchar(8000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BODY",
                schema: "ZPL",
                table: "RESOURCES",
                type: "varchar(8000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8192)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DELETED_AT",
                schema: "PRINT",
                table: "PALLETS",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IS_SHIPPED",
                schema: "PRINT",
                table: "PALLETS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "UQ_PALLETS__NUMBER",
                schema: "PRINT",
                table: "PALLETS",
                column: "NUMBER",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_PALLETS__NUMBER",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropColumn(
                name: "DELETED_AT",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.DropColumn(
                name: "IS_SHIPPED",
                schema: "PRINT",
                table: "PALLETS");

            migrationBuilder.AlterColumn<string>(
                name: "BODY",
                schema: "ZPL",
                table: "TEMPLATES",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8000)");

            migrationBuilder.AlterColumn<string>(
                name: "BODY",
                schema: "ZPL",
                table: "RESOURCES",
                type: "varchar(8192)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8000)");
        }
    }
}
