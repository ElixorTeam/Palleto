using Phetch.Core;
using Pl.Admin.Models;
using Pl.Admin.Models.Features.Database;
// ReSharper disable ClassNeverInstantiated.Global

namespace Pl.Admin.Client.Source.Shared.Api.Web.Endpoints;

public sealed class DiagnosticEndpoints(IWebApi webApi)
{
    public ParameterlessEndpoint<MigrationHistoryDto[]> MigrationsEndpoint { get; } = new(
        webApi.GetMigrations,
        options: new()
        {
            DefaultStaleTime = TimeSpan.FromMinutes(5),
        });

    public ParameterlessEndpoint<DataBaseTableDto[]> TablesEndpoint { get; } = new(
        webApi.GetTables,
        options: new()
        {
            DefaultStaleTime = TimeSpan.FromMinutes(5),
        });
}