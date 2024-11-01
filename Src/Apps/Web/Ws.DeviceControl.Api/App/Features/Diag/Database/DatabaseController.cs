using Ws.DeviceControl.Api.App.Features.Diag.Database.Common;
using Ws.DeviceControl.Models.Features.Database;

namespace Ws.DeviceControl.Api.App.Features.Diag.Database;

[ApiController]
[Authorize(PolicyEnum.Admin)]
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