using Ws.Database.Entities.Ref.Lines;
using Ws.Database.Entities.Ref.Printers;
using Ws.Database.Entities.Ref.Warehouses;
using Ws.Database.Entities.Ref1C.Plus;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Common;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Expressions;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Extensions;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Validators;
using Ws.DeviceControl.Api.App.Shared.Enums;
using Ws.DeviceControl.Models.Features.Devices.Arms.Commands;
using Ws.DeviceControl.Models.Features.Devices.Arms.Queries;
using Ws.Shared.ValueTypes;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl;

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
        return dbContext.Lines
            .AsNoTracking()
            .Where(i => i.Warehouse.ProductionSite.Id == prodSiteId)
            .OrderBy(i => i.Name)
            .Select(ArmExpressions.ToDto)
            .ToArrayAsync();
    }

    public async Task<ArmDto> GetByIdAsync(Guid id)
    {
        LineEntity entity = await dbContext.Lines.SafeGetById(id, FkProperty.Line);
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
            .Where(l => l.Line.Id == id && l.CreateDt.AddHours(3) >= workShift.Start
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
        LineEntity entity = await dbContext.Lines.SafeGetById(id, FkProperty.Line);

        bool? isWeightFilter = entity.Type switch
        {
            ArmType.Pc => false,
            ArmType.Tablet => true,
            _ => null
        };

        Guid[] linePluId = await dbContext.Lines
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
        await createValidator.ValidateAsync(dbContext.Lines, dto);

        PrinterEntity printer = await dbContext.Printers.SafeGetById(dto.PrinterId,  FkProperty.Printer);
        WarehouseEntity warehouse = await dbContext.Warehouses.SafeGetById(dto.WarehouseId, FkProperty.Warehouse);

        LineEntity entity = dto.ToEntity(warehouse, printer);

        await userHelper.ValidateUserProductionSiteAsync(warehouse.ProductionSiteId);

        await dbContext.Lines.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return await GetArmDto(entity);
    }

    public async Task<ArmDto> UpdateAsync(Guid id, ArmUpdateDto dto)
    {
        LineEntity entity = await updateValidator.ValidateAndGetAsync(dbContext.Lines, dto, id);

        PrinterEntity printer = await dbContext.Printers.SafeGetById(dto.PrinterId,  FkProperty.Printer);
        WarehouseEntity warehouse = await dbContext.Warehouses.SafeGetById(dto.WarehouseId, FkProperty.Warehouse);

        await userHelper.ValidateUserProductionSiteAsync(warehouse.ProductionSiteId);

        dto.UpdateEntity(entity, printer, warehouse);
        await dbContext.SaveChangesAsync();

        return await GetArmDto(entity);
    }

    public async Task DeletePluAsync(Guid armId, Guid pluId)
    {
        LineEntity line = await dbContext.Lines
          .Include(l => l.Plus)
          .FirstOrDefaultAsync(l => l.Id == armId)
                ?? throw new("АРМ не найдено");

        PluEntity plu = await dbContext.Plus.SafeGetById(pluId, FkProperty.Plu);
        line.Plus.Remove(plu);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddPluAsync(Guid armId, Guid pluId)
    {
        LineEntity line = await dbContext.Lines
          .Include(l => l.Plus)
          .FirstOrDefaultAsync(l => l.Id == armId)
                ?? throw new("АРМ не найдено");

        PluEntity plu = await dbContext.Plus.SafeGetById(pluId, FkProperty.Plu);

        if (line.Plus.Any(i => i.Id == pluId))
            return;

        line.Plus.Add(plu);

        await dbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(Guid id) => dbContext.Lines.SafeDeleteAsync(i => i.Id == id, FkProperty.Line);

    #endregion

    #region Private

    private async Task<ArmDto> GetArmDto(LineEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.Printer).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Warehouse).LoadAsync();
        await dbContext.Entry(entity.Warehouse).Reference(e => e.ProductionSite).LoadAsync();

        return ArmExpressions.ToDto.Compile().Invoke(entity);
    }

    #endregion
}