using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Replace_HostName_To_SystemId_ArmsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_ARMS__PC_NAME",
                schema: "REF",
                table: "ARMS");

            migrationBuilder.DropColumn(
                name: "PC_NAME",
                schema: "REF",
                table: "ARMS");

            migrationBuilder.AddColumn<Guid>(
                name: "SYSTEM_KEY",
                schema: "REF",
                table: "ARMS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.Sql("UPDATE [REF].[ARMS] SET SYSTEM_KEY = NEWID() WHERE SYSTEM_KEY = '00000000-0000-0000-0000-000000000000'");

            migrationBuilder.CreateIndex(
                name: "UQ_ARMS__SYSTEM_KEY",
                schema: "REF",
                table: "ARMS",
                column: "SYSTEM_KEY",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_ARMS__SYSTEM_KEY",
                schema: "REF",
                table: "ARMS");

            migrationBuilder.DropColumn(
                name: "SYSTEM_KEY",
                schema: "REF",
                table: "ARMS");

            migrationBuilder.AddColumn<string>(
                name: "PC_NAME",
                schema: "REF",
                table: "ARMS",
                type: "varchar(32)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UQ_ARMS__PC_NAME",
                schema: "REF",
                table: "ARMS",
                column: "PC_NAME",
                unique: true);
        }
    }
}
