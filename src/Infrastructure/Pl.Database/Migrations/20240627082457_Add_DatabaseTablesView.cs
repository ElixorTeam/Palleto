using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pl.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_DatabaseFilesView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DIAG");

            migrationBuilder.Sql(@"
            CREATE VIEW DIAG.DATABASE_TABLES_VIEW
            AS
           SELECT
                [S].[NAME] AS [SCHEMA],
                [T].[NAME] AS [TABLE],
                CAST([P].[ROWS] AS INT) AS [ROWS_COUNT],
                CAST(ROUND(SUM(NULLIF([A].[USED_PAGES], 0)) * 8 / 1024, 0) AS INT) AS [USED_MB],
                [F].[NAME] AS [FILE_NAME]
            FROM [SYS].[TABLES] AS [T]
            INNER JOIN [SYS].[INDEXES] AS [I] ON [T].[OBJECT_ID] = [I].[OBJECT_ID]
            INNER JOIN [SYS].[PARTITIONS] AS [P] ON [I].[OBJECT_ID] = [P].[OBJECT_ID] AND [I].[INDEX_ID] = [P].[INDEX_ID]
            INNER JOIN [SYS].[ALLOCATION_UNITS] AS [A] ON [P].[PARTITION_ID] = [A].[CONTAINER_ID]
            LEFT OUTER JOIN [SYS].[SCHEMAS] AS [S] ON [T].[SCHEMA_ID] = [S].[SCHEMA_ID]
            INNER JOIN [SYS].[DATABASE_FILES] AS [F] ON [A].[DATA_SPACE_ID] = [F].[DATA_SPACE_ID]
            WHERE [T].[IS_MS_SHIPPED] = 0
              AND [I].[OBJECT_ID] > 255
              AND LEFT([T].[NAME], 1) <> '_'
            GROUP BY [T].[NAME], [S].[NAME], [P].[ROWS], [F].[NAME]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW DIAG.DATABASE_TABLES_VIEW;");
        }
    }
}
