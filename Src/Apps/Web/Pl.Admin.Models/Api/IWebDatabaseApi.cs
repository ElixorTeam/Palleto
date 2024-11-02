using Pl.Admin.Models.Features.Database;

namespace Pl.Admin.Models.Api;

public interface IWebDatabaseApi
{
    [Get("/database/migrations")]
    Task<MigrationHistoryDto[]> GetMigrations();

    [Get("/database/tables")]
    Task<DataBaseTableDto[]> GetTables();
}