using Ws.DeviceControl.Api.App.Features.References1C.Brands.Common;
using Ws.DeviceControl.Api.App.Features.References1C.Brands.Impl.Expressions;
using Ws.DeviceControl.Api.App.Shared.Enums;
using Ws.DeviceControl.Models.Features.References1C.Brands;

namespace Ws.DeviceControl.Api.App.Features.References1C.Brands.Impl;

internal sealed class BrandApiService(WsDbContext dbContext) : IBrandService
{
    #region Queries

    public async Task<BrandDto> GetByIdAsync(Guid id) =>
        BrandExpressions.ToDto.Compile().Invoke(await dbContext.Brands.SafeGetById(id, FkProperty.Brand));

    public Task<List<BrandDto>> GetAllAsync()
    {
        return dbContext.Brands
            .AsNoTracking().Select(BrandExpressions.ToDto)
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    #endregion
}