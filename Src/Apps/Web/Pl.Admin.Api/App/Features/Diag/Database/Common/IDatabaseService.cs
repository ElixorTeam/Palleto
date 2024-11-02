using Pl.Admin.Models.Features.Database;

namespace Pl.Admin.Api.App.Features.Diag.Database.Common;

public interface IDatabaseService
{
    List<MigrationHistoryDto> GetAllMigrations();
    List<DataBaseTableDto> GetAllTables();
}