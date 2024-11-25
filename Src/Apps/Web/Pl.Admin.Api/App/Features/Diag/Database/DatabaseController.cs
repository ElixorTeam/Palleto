using Pl.Admin.Api.App.Features.Diag.Database.Common;
using Pl.Admin.Models.Features.Database;

namespace Pl.Admin.Api.App.Features.Diag.Database;

[ApiController]
[Authorize(PolicyEnum.Developer)]
[Route(ApiEndpoints.Database)]
public sealed class DatabaseController(IDatabaseService databaseService)
{
    #region Queries

    [HttpGet("migrations")]
    public List<MigrationHistoryDto> GetAllMigrations() =>
        databaseService.GetAllMigrations();

    [HttpGet("tables")]
    public List<DataBaseTableDto> GetAllTables() =>
        databaseService.GetAllTables();

    #endregion
}