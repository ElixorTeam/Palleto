using Ws.Database.EntityFramework.Entities.Ref.Printers;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Common;
using Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl.Expressions;
using Ws.DeviceControl.Models.Dto.Devices.Printers.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers.Impl;

public class PrinterApiService(WsDbContext dbContext) : IPrinterService
{
    #region Queries

    public Task<List<PrinterDto>> GetAllByProductionSiteAsync(Guid productionSiteId)
    {
        return dbContext.Printers
            .AsNoTracking()
            .Where(i => i.ProductionSite.Id == productionSiteId)
            .Select(PrinterExpressions.ToDto)
            .OrderBy(i => i.Type).ThenBy(i => i.Name)
            .ToListAsync();
    }

    public Task<List<ProxyDto>> GetProxiesByProductionSiteAsync(Guid productionSiteId)
    {
        return dbContext.Printers
            .Where(i => i.ProductionSite.Id == productionSiteId)
            .Select(PrinterExpressions.ToProxy)
            .OrderBy(i => i.Name)
            .ToListAsync();
    }

    public async Task<PrinterDto> GetByIdAsync(Guid id)
    {
        PrinterEntity? printer = await dbContext.Printers.FindAsync(id);
        if (printer == null) throw new KeyNotFoundException();
        return PrinterExpressions.ToDto.Compile().Invoke(printer);
    }

    #endregion
}