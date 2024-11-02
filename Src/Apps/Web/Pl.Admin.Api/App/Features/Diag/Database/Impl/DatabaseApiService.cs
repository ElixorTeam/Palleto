using System.Runtime.CompilerServices;
using Pl.Admin.Api.App.Features.Diag.Database.Common;
using Pl.Admin.Models.Features.Database;

namespace Pl.Admin.Api.App.Features.Diag.Database.Impl;

internal sealed class DatabaseApiService(WsDbContext dbContext) : IDatabaseService
{
    public List<MigrationHistoryDto> GetAllMigrations()
    {
        FormattableString getMigrationSql = FormattableStringFactory.Create("SELECT * FROM dbo.__EFMigrationsHistory");
        List<MigrationHistoryDto> data = dbContext.Database.SqlQuery<MigrationHistoryDto>(getMigrationSql).ToList();
        data.Reverse();
        return data;
    }

    public List<DataBaseTableDto> GetAllTables()
    {
        return dbContext.DatabaseTables.Select(i => new DataBaseTableDto
        {
            Schema = i.Schema,
            Table = i.Table,
            Rows = i.Rows,
            UsedMb = i.UsedMb ?? 0,
            FileName = i.FileName
        })
            .OrderBy(i => i.FileName)
            .ThenBy(i => i.Schema)
            .ThenByDescending(i => i.UsedMb)
            .ThenByDescending(i => i.Rows).ToList();
    }
}