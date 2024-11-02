using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Drop_ClaimsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USERS_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "USERS");

            migrationBuilder.DropTable(
                name: "USERS_CLAIMS_FK",
                schema: "REF");

            migrationBuilder.DropTable(
                name: "CLAIMS",
                schema: "REF");

            migrationBuilder.DropIndex(
                name: "UQ_USERS_NAME",
                schema: "REF",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "CREATE_DT",
                schema: "REF",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "LOGIN_DT",
                schema: "REF",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "NAME",
                schema: "REF",
                table: "USERS");

            migrationBuilder.AlterColumn<Guid>(
                name: "PRODUCTION_SITE_UID",
                schema: "REF",
                table: "USERS",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_USERS__PRODUCTION_SITE",
                schema: "REF",
                table: "USERS",
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
                name: "FK_USERS__PRODUCTION_SITE",
                schema: "REF",
                table: "USERS");

            migrationBuilder.AlterColumn<Guid>(
                name: "PRODUCTION_SITE_UID",
                schema: "REF",
                table: "USERS",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATE_DT",
                schema: "REF",
                table: "USERS",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "LOGIN_DT",
                schema: "REF",
                table: "USERS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NAME",
                schema: "REF",
                table: "USERS",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CLAIMS",
                schema: "REF",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    NAME = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLAIMS", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "USERS_CLAIMS_FK",
                schema: "REF",
                columns: table => new
                {
                    CLAIM_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USER_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS_CLAIMS_FK", x => new { x.CLAIM_UID, x.USER_UID });
                    table.ForeignKey(
                        name: "FK_USERS_CLAIMS_FK_CLAIMS_CLAIM_UID",
                        column: x => x.CLAIM_UID,
                        principalSchema: "REF",
                        principalTable: "CLAIMS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USERS_CLAIMS_FK_USERS_USER_UID",
                        column: x => x.USER_UID,
                        principalSchema: "REF",
                        principalTable: "USERS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_USERS_NAME",
                schema: "REF",
                table: "USERS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_CLAIMS_NAME",
                schema: "REF",
                table: "CLAIMS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_CLAIMS_FK_USER_UID",
                schema: "REF",
                table: "USERS_CLAIMS_FK",
                column: "USER_UID");

            migrationBuilder.AddForeignKey(
                name: "FK_USERS_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "USERS",
                column: "PRODUCTION_SITE_UID",
                principalSchema: "REF",
                principalTable: "PRODUCTION_SITES",
                principalColumn: "UID");
        }
    }
}
