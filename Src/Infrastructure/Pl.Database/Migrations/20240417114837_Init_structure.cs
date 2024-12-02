using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init_structure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "REF");

            migrationBuilder.EnsureSchema(
                name: "REF_1C");

            migrationBuilder.EnsureSchema(
                name: "PRINT");

            migrationBuilder.EnsureSchema(
                name: "ZPL");

            migrationBuilder.CreateTable(
                name: "BOXES",
                schema: "REF_1C",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    WEIGHT = table.Column<decimal>(type: "decimal(4,3)", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOXES", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "BRANDS",
                schema: "REF_1C",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BRANDS", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "BUNDLES",
                schema: "REF_1C",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    WEIGHT = table.Column<decimal>(type: "decimal(4,3)", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BUNDLES", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "CLAIMS",
                schema: "REF",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLAIMS", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "CLIPS",
                schema: "REF_1C",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    WEIGHT = table.Column<decimal>(type: "decimal(4,3)", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIPS", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "PALLET_MEN",
                schema: "REF",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    SURNAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PATRONYMIC = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PALLET_MEN", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTION_SITES",
                schema: "REF",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTION_SITES", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "RESOURCES",
                schema: "ZPL",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ZPL = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESOURCES", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "STORAGE_METHODS",
                schema: "ZPL",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ZPL = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STORAGE_METHODS", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "TEMPLATES",
                schema: "ZPL",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BODY = table.Column<string>(type: "nvarchar(max)", maxLength: 10240, nullable: false),
                    IS_WEIGHT = table.Column<bool>(type: "bit", nullable: false),
                    WIDTH = table.Column<short>(type: "smallint", nullable: false),
                    HEIGHT = table.Column<short>(type: "smallint", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPLATES", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "PLUS",
                schema: "REF_1C",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FULL_NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NUMBER = table.Column<short>(type: "smallint", nullable: false),
                    SHELF_LIFE_DAYS = table.Column<short>(type: "smallint", nullable: false),
                    EAN_13 = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    ITF_14 = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    IS_WEIGHT = table.Column<bool>(type: "bit", nullable: false),
                    WEIGHT = table.Column<decimal>(type: "decimal(4,3)", nullable: false),
                    STORAGE_METHOD = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BUNDLE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BRAND_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CLIP_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TEMPLATE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PLUS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_PLUS_BRANDS_BRAND_UID",
                        column: x => x.BRAND_UID,
                        principalSchema: "REF_1C",
                        principalTable: "BRANDS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLUS_BUNDLES_BUNDLE_UID",
                        column: x => x.BUNDLE_UID,
                        principalSchema: "REF_1C",
                        principalTable: "BUNDLES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PLUS_CLIPS_CLIP_UID",
                        column: x => x.CLIP_UID,
                        principalSchema: "REF_1C",
                        principalTable: "CLIPS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRINTERS",
                schema: "REF",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    IP_V4 = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    TYPE = table.Column<string>(type: "varchar(8)", nullable: false),
                    PRODUCTION_SITE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRINTERS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_PRINTERS_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                        column: x => x.PRODUCTION_SITE_UID,
                        principalSchema: "REF",
                        principalTable: "PRODUCTION_SITES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                schema: "REF",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PRODUCTION_SITE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LOGIN_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_USERS_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                        column: x => x.PRODUCTION_SITE_UID,
                        principalSchema: "REF",
                        principalTable: "PRODUCTION_SITES",
                        principalColumn: "UID");
                });

            migrationBuilder.CreateTable(
                name: "WAREHOUSES",
                schema: "REF",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PRODUCTION_SITE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WAREHOUSES", x => x.UID);
                    table.ForeignKey(
                        name: "FK_WAREHOUSES_PRODUCTION_SITES_PRODUCTION_SITE_UID",
                        column: x => x.PRODUCTION_SITE_UID,
                        principalSchema: "REF",
                        principalTable: "PRODUCTION_SITES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHARACTERISTICS",
                schema: "REF_1C",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BUNDLE_COUNT = table.Column<short>(type: "smallint", nullable: false),
                    BOX_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PLU_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHARACTERISTICS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_CHARACTERISTICS_BOXES_BOX_UID",
                        column: x => x.BOX_UID,
                        principalSchema: "REF_1C",
                        principalTable: "BOXES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHARACTERISTICS_PLUS_PLU_UID",
                        column: x => x.PLU_UID,
                        principalSchema: "REF_1C",
                        principalTable: "PLUS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NESTINGS",
                schema: "REF_1C",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BUNDLE_COUNT = table.Column<short>(type: "smallint", nullable: false),
                    BOX_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NESTINGS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_NESTINGS_BOXES_BOX_UID",
                        column: x => x.BOX_UID,
                        principalSchema: "REF_1C",
                        principalTable: "BOXES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NESTINGS_PLUS_UID",
                        column: x => x.UID,
                        principalSchema: "REF_1C",
                        principalTable: "PLUS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "ARMS",
                schema: "REF",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    COUNTER = table.Column<int>(type: "int", nullable: false),
                    NUMBER = table.Column<int>(type: "int", nullable: false),
                    WAREHOUSE_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PRINTER_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PC_NAME = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    TYPE = table.Column<string>(type: "varchar(12)", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CHANGE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARMS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_ARMS_PRINTERS_PRINTER_UID",
                        column: x => x.PRINTER_UID,
                        principalSchema: "REF",
                        principalTable: "PRINTERS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ARMS_WAREHOUSES_WAREHOUSE_UID",
                        column: x => x.WAREHOUSE_UID,
                        principalSchema: "REF",
                        principalTable: "WAREHOUSES",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ARMS_PLUS_FK",
                columns: table => new
                {
                    PLU_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ARM_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ARMS_PLUS_FK", x => new { x.PLU_UID, x.ARM_UID });
                    table.ForeignKey(
                        name: "FK_ARMS_PLUS_FK_ARMS_ARM_UID",
                        column: x => x.ARM_UID,
                        principalSchema: "REF",
                        principalTable: "ARMS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ARMS_PLUS_FK_PLUS_PLU_UID",
                        column: x => x.PLU_UID,
                        principalSchema: "REF_1C",
                        principalTable: "PLUS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LABELS",
                schema: "PRINT",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZPL = table.Column<string>(type: "nvarchar(max)", maxLength: 10240, nullable: false),
                    BARCODE_TOP = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    BARCODE_RIGHT = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    BARCODE_BOTTOM = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    WEIGHT_NET = table.Column<decimal>(type: "decimal(4,3)", nullable: false),
                    WEIGHT_TARE = table.Column<decimal>(type: "decimal(4,3)", nullable: false),
                    KNEADING = table.Column<short>(type: "smallint", nullable: false),
                    PLU_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ARM_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PRODUCT_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EXPIRATION_DT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CREATE_DT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LABELS", x => x.UID);
                    table.ForeignKey(
                        name: "FK_LABELS_ARMS_ARM_UID",
                        column: x => x.ARM_UID,
                        principalSchema: "REF",
                        principalTable: "ARMS",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LABELS_PLUS_PLU_UID",
                        column: x => x.PLU_UID,
                        principalSchema: "REF_1C",
                        principalTable: "PLUS",
                        principalColumn: "UID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ARMS_PRINTER_UID",
                schema: "REF",
                table: "ARMS",
                column: "PRINTER_UID");

            migrationBuilder.CreateIndex(
                name: "IX_ARMS_WAREHOUSE_UID",
                schema: "REF",
                table: "ARMS",
                column: "WAREHOUSE_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_ARMS_NAME",
                schema: "REF",
                table: "ARMS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_ARMS_NUMBER",
                schema: "REF",
                table: "ARMS",
                column: "NUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_ARMS_PC_NAME",
                schema: "REF",
                table: "ARMS",
                column: "PC_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ARMS_PLUS_FK_ARM_UID",
                table: "ARMS_PLUS_FK",
                column: "ARM_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_BRANDS_NAME",
                schema: "REF_1C",
                table: "BRANDS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CHARACTERISTICS_BOX_UID",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                column: "BOX_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_CHARACTERISTICS_UNIQ",
                schema: "REF_1C",
                table: "CHARACTERISTICS",
                columns: new[] { "PLU_UID", "BOX_UID", "BUNDLE_COUNT" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_CLAIMS_NAME",
                schema: "REF",
                table: "CLAIMS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LABELS_ARM_UID",
                schema: "PRINT",
                table: "LABELS",
                column: "ARM_UID");

            migrationBuilder.CreateIndex(
                name: "IX_LABELS_PLU_UID",
                schema: "PRINT",
                table: "LABELS",
                column: "PLU_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_PLUS_BARCODE_TOP",
                schema: "PRINT",
                table: "LABELS",
                column: "BARCODE_TOP",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NESTINGS_BOX_UID",
                schema: "REF_1C",
                table: "NESTINGS",
                column: "BOX_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_PALLET_MEN_FIO",
                schema: "REF",
                table: "PALLET_MEN",
                columns: new[] { "NAME", "SURNAME", "PATRONYMIC" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_BRAND_UID",
                schema: "REF_1C",
                table: "PLUS",
                column: "BRAND_UID");

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_BUNDLE_UID",
                schema: "REF_1C",
                table: "PLUS",
                column: "BUNDLE_UID");

            migrationBuilder.CreateIndex(
                name: "IX_PLUS_CLIP_UID",
                schema: "REF_1C",
                table: "PLUS",
                column: "CLIP_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_PLUS_NUMBER",
                schema: "REF_1C",
                table: "PLUS",
                column: "NUMBER",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRINTERS_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "PRINTERS",
                column: "PRODUCTION_SITE_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_PRINTERS_IP_V4",
                schema: "REF",
                table: "PRINTERS",
                column: "IP_V4",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PRINTERS_NAME",
                schema: "REF",
                table: "PRINTERS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PRODUCTION_SITES_ADDRESS",
                schema: "REF",
                table: "PRODUCTION_SITES",
                column: "ADDRESS",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_PRODUCTION_SITES_NAME",
                schema: "REF",
                table: "PRODUCTION_SITES",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_RESOURCES_NAME",
                schema: "ZPL",
                table: "RESOURCES",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_STORAGE_METHODS_NAME",
                schema: "ZPL",
                table: "STORAGE_METHODS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_STORAGE_METHODS_ZPL",
                schema: "ZPL",
                table: "STORAGE_METHODS",
                column: "ZPL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TEMPLATES_NAME_IS_WEIGHT",
                schema: "ZPL",
                table: "TEMPLATES",
                columns: new[] { "NAME", "IS_WEIGHT" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "USERS",
                column: "PRODUCTION_SITE_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_USERS_NAME",
                schema: "REF",
                table: "USERS",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_CLAIMS_FK_USER_UID",
                schema: "REF",
                table: "USERS_CLAIMS_FK",
                column: "USER_UID");

            migrationBuilder.CreateIndex(
                name: "IX_WAREHOUSES_PRODUCTION_SITE_UID",
                schema: "REF",
                table: "WAREHOUSES",
                column: "PRODUCTION_SITE_UID");

            migrationBuilder.CreateIndex(
                name: "UQ_WAREHOUSES_NAME",
                schema: "REF",
                table: "WAREHOUSES",
                column: "NAME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ARMS_PLUS_FK");

            migrationBuilder.DropTable(
                name: "CHARACTERISTICS",
                schema: "REF_1C");

            migrationBuilder.DropTable(
                name: "LABELS",
                schema: "PRINT");

            migrationBuilder.DropTable(
                name: "NESTINGS",
                schema: "REF_1C");

            migrationBuilder.DropTable(
                name: "PALLET_MEN",
                schema: "REF");

            migrationBuilder.DropTable(
                name: "RESOURCES",
                schema: "ZPL");

            migrationBuilder.DropTable(
                name: "STORAGE_METHODS",
                schema: "ZPL");

            migrationBuilder.DropTable(
                name: "TEMPLATES",
                schema: "ZPL");

            migrationBuilder.DropTable(
                name: "USERS_CLAIMS_FK",
                schema: "REF");

            migrationBuilder.DropTable(
                name: "ARMS",
                schema: "REF");

            migrationBuilder.DropTable(
                name: "BOXES",
                schema: "REF_1C");

            migrationBuilder.DropTable(
                name: "PLUS",
                schema: "REF_1C");

            migrationBuilder.DropTable(
                name: "CLAIMS",
                schema: "REF");

            migrationBuilder.DropTable(
                name: "USERS",
                schema: "REF");

            migrationBuilder.DropTable(
                name: "PRINTERS",
                schema: "REF");

            migrationBuilder.DropTable(
                name: "WAREHOUSES",
                schema: "REF");

            migrationBuilder.DropTable(
                name: "BRANDS",
                schema: "REF_1C");

            migrationBuilder.DropTable(
                name: "BUNDLES",
                schema: "REF_1C");

            migrationBuilder.DropTable(
                name: "CLIPS",
                schema: "REF_1C");

            migrationBuilder.DropTable(
                name: "PRODUCTION_SITES",
                schema: "REF");
        }
    }
}