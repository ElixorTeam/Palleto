using Ws.Database.Entities.Ref.PalletMen;
using Ws.Database.Entities.Ref.Warehouses;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Common;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Extensions;
using Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl.Validators;
using Ws.DeviceControl.Api.App.Shared.Enums;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Impl;

internal sealed class PalletManApiService(
    UserHelper userHelper,
    WsDbContext dbContext,
    PalletManCreateApiValidator createValidator,
    PalletManUpdateApiValidator updateValidator
    ) : IPalletManService
{
    #region Queries

    public Task<List<PalletManDto>> GetAllByProductionSiteAsync(Guid productionSiteId)
    {
        return dbContext.PalletMen
            .AsNoTracking()
            .Where(i => i.Warehouse.ProductionSite.Id == productionSiteId)
            .Select(PalletManExpressions.ToDto)
            .ToListAsync();
    }

    public async Task<PalletManDto> GetByIdAsync(Guid id)
    {
        PalletManEntity palletMan = await dbContext.PalletMen.SafeGetById(id, FkProperty.PalletMan);
        return await GetPalletManDtoDto(palletMan);
    }

    #endregion

    #region Commands

    public async Task<PalletManDto> CreateAsync(PalletManCreateDto dto)
    {
        await createValidator.ValidateAsync(dbContext.PalletMen, dto);

        WarehouseEntity warehouse = await dbContext.Warehouses.SafeGetById(dto.WarehouseId, FkProperty.Warehouse);
        await userHelper.ValidateUserProductionSiteAsync(warehouse.ProductionSiteId);

        PalletManEntity entity = dto.ToEntity(warehouse);

        await dbContext.PalletMen.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return await GetPalletManDtoDto(entity);
    }

    public async Task<PalletManDto> UpdateAsync(Guid id, PalletManUpdateDto dto)
    {
        PalletManEntity entity =  await updateValidator.ValidateAndGetAsync(dbContext.PalletMen, dto, id);

        WarehouseEntity warehouse = await dbContext.Warehouses.SafeGetById(dto.WarehouseId, FkProperty.Warehouse);

        await userHelper.ValidateUserProductionSiteAsync(warehouse.ProductionSiteId);

        dto.UpdateEntity(entity, warehouse);
        await dbContext.SaveChangesAsync();

        return await GetPalletManDtoDto(entity);
    }

    public Task DeleteAsync(Guid id) => dbContext.PalletMen.SafeDeleteAsync(i => i.Id == id, FkProperty.PalletMan);

    #endregion

    #region Private

    private async Task<PalletManDto> GetPalletManDtoDto(PalletManEntity palletMan)
    {
        await dbContext.Entry(palletMan).Reference(e => e.Warehouse).LoadAsync();
        await dbContext.Entry(palletMan.Warehouse).Reference(e => e.ProductionSite).LoadAsync();

        return PalletManExpressions.ToDto.Compile().Invoke(palletMan);
    }

    #endregion
}