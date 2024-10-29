using Ws.DeviceControl.Api.App.Features.References1C.Boxes.Common;
using Ws.DeviceControl.Api.App.Features.References1C.Boxes.Impl.Expressions;
using Ws.DeviceControl.Api.App.Shared.Enums;

namespace Ws.DeviceControl.Api.App.Features.References1C.Boxes.Impl;

internal sealed class BoxApiService(WsDbContext dbContext) : IBoxService
{
    #region Queries

    public async Task<PackageDto> GetByIdAsync(Guid id) =>
        BoxExpressions.ToDto.Compile().Invoke(await dbContext.Boxes.SafeGetById(id, FkProperty.Box));

    public Task<List<PackageDto>> GetAllAsync()
    {
        return dbContext.Boxes
            .AsNoTracking().Select(BoxExpressions.ToDto)
            .OrderBy(i => i.Weight).ThenBy(i => i.Name)
            .ToListAsync();
    }

    #endregion
}