using Pl.Admin.Api.App.Features.Devices.Arms.Common;
using Pl.Admin.Api.App.Features.Devices.Arms.Impl.Expressions;
using Pl.Admin.Api.App.Features.Devices.Arms.Impl.Validators;
using Pl.Admin.Api.App.Shared.Enums;
using Pl.Database.Entities.Ref.Printers;
using Pl.Database.Entities.Ref.Warehouses;
using Pl.Database.Entities.Ref1C.Plus;
using Pl.Admin.Api.App.Features.Devices.Arms.Impl.Extensions;
using Pl.Admin.Models.Features.Devices.Arms.Commands;
using Pl.Admin.Models.Features.Devices.Arms.Queries;
using Pl.Database.Entities.Ref.Arms;
using Pl.Shared.ValueTypes;

namespace Pl.Admin.Api.App.Features.Devices.Arms.Impl;

internal sealed class ArmApiService(
    WsDbContext dbContext,
    ArmCreateApiValidator createValidator,
    ArmUpdateApiValidator updateValidator,
    UserHelper userHelper
    ) : IArmService
{
    #region Queries

    public Task<ArmDto[]> GetAllByProdSiteAsync(Guid prodSiteId)
    {
        return dbContext.Arms
            .AsNoTracking()
            .Where(i => i.Warehouse.ProductionSite.Id == prodSiteId)
            .OrderBy(i => i.Name)
            .Select(ArmExpressions.ToDto)
            .ToArrayAsync();
    }

    public async Task<ArmDto> GetByIdAsync(Guid id)
    {
        ArmEntity entity = await dbContext.Arms.SafeGetById(id, FkProperty.Arm);
        return await GetArmDto(entity);
    }

    public async Task<AnalyticDto[]> GetAnalyticAsync(Guid id, DateOnly date)
    {
        WorkShift workShift = new(date);

        List<DateTime> hourlyRanges = Enumerable
            .Range(0, 24)
            .Select(i => workShift.Start.AddHours(i))
            .ToList();

        List<AnalyticDto> labelCounts = await dbContext.Labels
            .Where(l => l.Arm.Id == id && l.CreateDt.AddHours(3) >= workShift.Start
                                        && l.CreateDt.AddHours(3) < workShift.End)
            .GroupBy(l => new
            {
                CreateDateHour = new DateTime(l.CreateDt.Year, l.CreateDt.Month, l.CreateDt.Day, l.CreateDt.AddHours(3).Hour, 0, 0)
            })
            .Select(g => new AnalyticDto(g.Key.CreateDateHour, (uint)g.Count()))
            .ToListAsync();

        return hourlyRanges.Select(hr =>
            new AnalyticDto(hr, labelCounts.FirstOrDefault(l => l.Date == hr)?.Count ?? 0)).ToArray();
    }

    public async Task<PluArmDto[]> GetPlusAsync(Guid id)
    {
        ArmEntity entity = await dbContext.Arms.SafeGetById(id, FkProperty.Arm);

        bool? isWeightFilter = entity.Type switch
        {
            ArmType.Pc => false,
            ArmType.Tablet => true,
            _ => null
        };

        Guid[] linePluId = await dbContext.Arms
            .AsNoTracking()
            .Where(i => i.Id == id)
            .SelectMany(i => i.Plus)
            .Select(i => i.Id)
            .ToArrayAsync();

        return await dbContext.Plus
            .AsNoTracking()
            .IfWhere(isWeightFilter != null, p => p.IsWeight == isWeightFilter)
            .OrderBy(i => i.Number)
            .Select(ArmExpressions.ToPluDto(linePluId))
            .ToArrayAsync();
    }

    #endregion

    #region Commands

    public async Task<ArmDto> CreateAsync(ArmCreateDto dto)
    {
        await createValidator.ValidateAsync(dbContext.Arms, dto);

        PrinterEntity printer = await dbContext.Printers.SafeGetById(dto.PrinterId,  FkProperty.Printer);
        WarehouseEntity warehouse = await dbContext.Warehouses.SafeGetById(dto.WarehouseId, FkProperty.Warehouse);

        ArmEntity entity = dto.ToEntity(warehouse, printer);

        await userHelper.ValidateUserProductionSiteAsync(warehouse.ProductionSiteId);

        await dbContext.Arms.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return await GetArmDto(entity);
    }

    public async Task<ArmDto> UpdateAsync(Guid id, ArmUpdateDto dto)
    {
        ArmEntity entity = await updateValidator.ValidateAndGetAsync(dbContext.Arms, dto, id);

        PrinterEntity printer = await dbContext.Printers.SafeGetById(dto.PrinterId,  FkProperty.Printer);
        WarehouseEntity warehouse = await dbContext.Warehouses.SafeGetById(dto.WarehouseId, FkProperty.Warehouse);

        await userHelper.ValidateUserProductionSiteAsync(warehouse.ProductionSiteId);

        dto.UpdateEntity(entity, printer, warehouse);
        await dbContext.SaveChangesAsync();

        return await GetArmDto(entity);
    }

    public async Task DeletePluAsync(Guid armId, Guid pluId)
    {
        ArmEntity arm = await dbContext.Arms
          .Include(l => l.Plus)
          .Include(l => l.Warehouse)
          .FirstOrDefaultAsync(l => l.Id == armId)
                ?? throw new("АРМ не найдено");

        await userHelper.ValidateUserProductionSiteAsync(arm.Warehouse.ProductionSiteId);

        PluEntity plu = await dbContext.Plus.SafeGetById(pluId, FkProperty.Plu);
        arm.Plus.Remove(plu);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddPluAsync(Guid armId, Guid pluId)
    {
        ArmEntity arm = await dbContext.Arms
          .Include(l => l.Plus)
          .Include(l => l.Warehouse)
          .FirstOrDefaultAsync(l => l.Id == armId)
                ?? throw new("АРМ не найдено");

        await userHelper.ValidateUserProductionSiteAsync(arm.Warehouse.ProductionSiteId);

        PluEntity plu = await dbContext.Plus.SafeGetById(pluId, FkProperty.Plu);

        if (arm.Plus.Any(i => i.Id == pluId))
            return;

        arm.Plus.Add(plu);

        await dbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(Guid id) => dbContext.Arms.SafeDeleteAsync(i => i.Id == id, FkProperty.Arm);

    #endregion

    #region Private

    private async Task<ArmDto> GetArmDto(ArmEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.Printer).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Warehouse).LoadAsync();
        await dbContext.Entry(entity.Warehouse).Reference(e => e.ProductionSite).LoadAsync();

        return ArmExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion
}