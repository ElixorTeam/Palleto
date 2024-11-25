using Pl.Admin.Api.App.Features.Devices.Printers.Common;
using Pl.Admin.Api.App.Features.Devices.Printers.Impl.Expressions;
using Pl.Admin.Api.App.Features.Devices.Printers.Impl.Validators;
using Pl.Admin.Api.App.Shared.Enums;
using Pl.Database.Entities.Ref.Printers;
using Pl.Database.Entities.Ref.ProductionSites;
using Pl.Admin.Api.App.Features.Devices.Printers.Impl.Extensions;
using Pl.Admin.Models.Features.Devices.Printers.Commands;
using Pl.Admin.Models.Features.Devices.Printers.Queries;

namespace Pl.Admin.Api.App.Features.Devices.Printers.Impl;

internal sealed class PrinterApiService(
    WsDbContext dbContext,
    UserHelper userHelper,
    PrinterCreateApiValidator createValidator,
    PrinterUpdateApiValidator updateValidator
    ) : IPrinterService
{
    #region Queries

    public Task<PrinterDto[]> GetAllByProdSiteAsync(Guid prodSiteId)
    {
        return dbContext.Printers
            .AsNoTracking()
            .Where(i => i.ProductionSite.Id == prodSiteId)
            .OrderBy(i => i.Type).ThenBy(i => i.Name)
            .Select(PrinterExpressions.ToDto)
            .ToArrayAsync();
    }

    public Task<ProxyDto[]> GetProxiesByProdSiteAsync(Guid prodSiteId)
    {
        return dbContext.Printers
            .Where(i => i.ProductionSite.Id == prodSiteId)
            .OrderBy(i => i.Name)
            .Select(PrinterExpressions.ToProxy)
            .ToArrayAsync();
    }

    public async Task<PrinterDto> GetByIdAsync(Guid id)
    {
        PrinterEntity entity =
            await dbContext.Printers.SafeGetById(id,FkProperty.Printer);

        return await GetPrinterDto(entity);
    }

    #endregion

    #region Commands

    public async Task<PrinterDto> CreateAsync(PrinterCreateDto dto)
    {
        await createValidator.ValidateAsync(dbContext.Printers, dto);

        ProductionSiteEntity productionSite = await dbContext.ProductionSites.SafeGetById(dto.ProductionSiteId,FkProperty.ProductionSite);
        await userHelper.ValidateUserProductionSiteAsync(productionSite.Id);

        PrinterEntity entity = dto.ToEntity(productionSite);

        await dbContext.Printers.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return PrinterExpressions.ToDto.Compile().Invoke(entity);
    }

    public async Task<PrinterDto> UpdateAsync(Guid id, PrinterUpdateDto dto)
    {
        PrinterEntity entity = await updateValidator.ValidateAndGetAsync(dbContext.Printers, dto, id);

        await userHelper.ValidateUserProductionSiteAsync(entity.ProductionSiteId);

        dto.UpdateEntity(entity);
        await dbContext.SaveChangesAsync();

        return PrinterExpressions.ToDto.Compile().Invoke(entity);
    }

    public Task DeleteAsync(Guid id) => dbContext.Printers.SafeDeleteAsync(i => i.Id == id, FkProperty.Printer);

    #endregion

    #region Private

    private async Task<PrinterDto> GetPrinterDto(PrinterEntity printer)
    {
        await dbContext.Entry(printer).Reference(e => e.ProductionSite).LoadAsync();
        return PrinterExpressions.ToDto.Compile().Invoke(printer);
    }

    #endregion
}