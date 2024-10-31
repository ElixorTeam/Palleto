using Ws.DeviceControl.Api.App.Features.References1C.Bundles.Common;
using Ws.DeviceControl.Api.App.Features.References1C.Bundles.Impl.Expressions;
using Ws.DeviceControl.Api.App.Shared.Enums;

namespace Ws.DeviceControl.Api.App.Features.References1C.Bundles.Impl;

internal sealed class BundleApiService(WsDbContext dbContext) : IBundleService
{
    #region Queries

    public async Task<PackageDto> GetByIdAsync(Guid id) =>
        BundleExpressions.ToDto.Compile().Invoke(await dbContext.Bundles.SafeGetById(id, FkProperty.Bundle));

    public Task<PackageDto[]> GetAllAsync()
    {
        return dbContext.Bundles
            .AsNoTracking()
            .OrderBy(i => i.Weight).ThenBy(i => i.Name)
            .Select(BundleExpressions.ToDto)
            .ToArrayAsync();
    }

    #endregion
}