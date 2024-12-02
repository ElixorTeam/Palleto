using Pl.Admin.Api.App.Features.References1C.Brands.Common;
using Pl.Admin.Api.App.Features.References1C.Brands.Impl.Expressions;
using Pl.Admin.Api.App.Shared.Enums;
using Pl.Admin.Models.Features.References1C.Brands;

namespace Pl.Admin.Api.App.Features.References1C.Brands.Impl;

internal sealed class BrandApiService(WsDbContext dbContext) : IBrandService
{
    #region Queries

    public async Task<BrandDto> GetByIdAsync(Guid id) =>
        BrandExpressions.ToDto.Compile().Invoke(await dbContext.Brands.SafeGetById(id, FkProperty.Brand));

    public Task<BrandDto[]> GetAllAsync()
    {
        return dbContext.Brands
            .AsNoTracking()
            .OrderBy(i => i.Name)
            .Select(BrandExpressions.ToDto)
            .ToArrayAsync();
    }

    #endregion
}