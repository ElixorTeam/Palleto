using Pl.Admin.Api.App.Features.References1C.Boxes.Common;
using Pl.Admin.Api.App.Features.References1C.Boxes.Impl.Expressions;
using Pl.Admin.Api.App.Shared.Enums;

namespace Pl.Admin.Api.App.Features.References1C.Boxes.Impl;

internal sealed class BoxApiService(WsDbContext dbContext) : IBoxService
{
    #region Queries

    public async Task<PackageDto> GetByIdAsync(Guid id) =>
        BoxExpressions.ToDto.Compile().Invoke(await dbContext.Boxes.SafeGetById(id, FkProperty.Box));

    public Task<PackageDto[]> GetAllAsync()
    {
        return dbContext.Boxes
            .AsNoTracking()
            .OrderBy(i => i.Weight).ThenBy(i => i.Name)
            .Select(BoxExpressions.ToDto)
            .ToArrayAsync();
    }

    #endregion
}