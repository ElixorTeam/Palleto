using Ws.DeviceControl.Api.App.Features.References1C.Clips.Common;
using Ws.DeviceControl.Api.App.Features.References1C.Clips.Impl.Expressions;
using Ws.DeviceControl.Api.App.Shared.Enums;

namespace Ws.DeviceControl.Api.App.Features.References1C.Clips.Impl;

internal sealed class ClipApiService(WsDbContext dbContext) : IClipService
{
    #region Queries

    public async Task<PackageDto> GetByIdAsync(Guid id) =>
        ClipExpressions.ToDto.Compile().Invoke(await dbContext.Clips.SafeGetById(id, FkProperty.Clip));

    public Task<PackageDto[]> GetAllAsync()
    {
        return dbContext.Clips
            .AsNoTracking()
            .OrderBy(i => i.Weight).ThenBy(i => i.Name)
            .Select(ClipExpressions.ToDto)
            .ToArrayAsync();
    }

    #endregion
}