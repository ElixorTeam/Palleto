using Pl.Admin.Api.App.Features.References.Warehouses.Common;
using Pl.Admin.Api.App.Features.References.Warehouses.Impl.Expressions;
using Pl.Admin.Api.App.Features.References.Warehouses.Impl.Validators;
using Pl.Admin.Api.App.Shared.Enums;
using Pl.Database.Entities.Ref.ProductionSites;
using Pl.Database.Entities.Ref.Warehouses;
using Pl.Admin.Api.App.Features.References.Warehouses.Impl.Extensions;
using Pl.Admin.Models.Features.References.Warehouses.Commands;
using Pl.Admin.Models.Features.References.Warehouses.Queries;

namespace Pl.Admin.Api.App.Features.References.Warehouses.Impl;

internal sealed class WarehouseApiService(
    WsDbContext dbContext,
    UserHelper userHelper,
    WarehouseCreateApiValidator createValidator,
    WarehouseUpdateApiValidator updateValidator
    ) : IWarehouseService
{
    #region Queries

    public Task<WarehouseDto[]> GetAllByProdSiteAsync(Guid productionSiteId)
    {
        return dbContext.Warehouses
            .AsNoTracking()
            .Where(i => i.ProductionSite.Id == productionSiteId)
            .OrderBy(i => i.Name)
            .Select(WarehouseExpressions.ToDto)
            .ToArrayAsync();
    }

    public Task<ProxyDto[]> GetProxiesByProdSiteAsync(Guid productionSiteId)
    {
        return dbContext.Warehouses
            .Where(i => i.ProductionSite.Id == productionSiteId)
            .OrderBy(i => i.Name)
            .Select(WarehouseExpressions.ToProxy)
            .ToArrayAsync();
    }

    public async Task<WarehouseDto> GetByIdAsync(Guid id)
    {
        WarehouseEntity entity = await dbContext.Warehouses.SafeGetById(id, FkProperty.Warehouse);
        return await GetWarehouseDto(entity);
    }

    #endregion

    #region Commands

    public async Task<WarehouseDto> CreateAsync(WarehouseCreateDto dto)
    {
        await createValidator.ValidateAsync(dbContext.Warehouses, dto);

        ProductionSiteEntity productionSiteEntity = await dbContext.ProductionSites.SafeGetById(dto.ProductionSiteId, FkProperty.Warehouse);

        WarehouseEntity entity = dto.ToEntity(productionSiteEntity);
        await userHelper.ValidateUserProductionSiteAsync(entity.ProductionSiteId);

        await dbContext.Warehouses.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return await GetWarehouseDto(entity);
    }

    public async Task<WarehouseDto> UpdateAsync(Guid id, WarehouseUpdateDto dto)
    {
        WarehouseEntity entity = await updateValidator.ValidateAndGetAsync(dbContext.Warehouses, dto, id);
        await userHelper.ValidateUserProductionSiteAsync(entity.ProductionSiteId);

        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return await GetWarehouseDto(entity);
    }

    public Task DeleteAsync(Guid id) => dbContext.Warehouses.SafeDeleteAsync(i => i.Id == id, FkProperty.Warehouse);

    #endregion

    #region Private

    private async Task<WarehouseDto> GetWarehouseDto(WarehouseEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.ProductionSite).LoadAsync();
        return WarehouseExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion
}