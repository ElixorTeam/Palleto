using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Update_ColumnsLength_in_LabelsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ZPL",
                schema: "PRINT",
                table: "LABELS",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 10240);

            migrationBuilder.AlterColumn<decimal>(
                name: "WEIGHT_TARE",
                schema: "PRINT",
                table: "LABELS",
                type: "decimal(5,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "WEIGHT_NET",
                schema: "PRINT",
                table: "LABELS",
                type: "decimal(5,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,3)");

            migrationBuilder.AlterColumn<string>(
                name: "BARCODE_TOP",
                schema: "PRINT",
                table: "LABELS",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "BARCODE_RIGHT",
                schema: "PRINT",
                table: "LABELS",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "BARCODE_BOTTOM",
                schema: "PRINT",
                table: "LABELS",
                type: "varchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "VERSION",
                schema: "REF",
                table: "ARMS",
                type: "varchar(16)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ZPL",
                schema: "PRINT",
                table: "LABELS",
                type: "nvarchar(max)",
                maxLength: 10240,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "WEIGHT_TARE",
                schema: "PRINT",
                table: "LABELS",
                type: "decimal(4,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "WEIGHT_NET",
                schema: "PRINT",
                table: "LABELS",
                type: "decimal(4,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,3)");

            migrationBuilder.AlterColumn<string>(
                name: "BARCODE_TOP",
                schema: "PRINT",
                table: "LABELS",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "BARCODE_RIGHT",
                schema: "PRINT",
                table: "LABELS",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "BARCODE_BOTTOM",
                schema: "PRINT",
                table: "LABELS",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "VERSION",
                schema: "REF",
                table: "ARMS",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)");
        }
    }
}