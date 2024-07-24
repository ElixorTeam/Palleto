using Ws.DeviceControl.Models.Dto.Devices.Printers.Commands.Create;
using Ws.DeviceControl.Models.Dto.Devices.Printers.Commands.Update;
using Ws.DeviceControl.Models.Dto.Devices.Printers.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers.Common;

public interface IPrinterService
{
    #region Queries

    Task<PrinterDto> GetByIdAsync(Guid id);
    Task<List<PrinterDto>> GetAllByProductionSiteAsync(Guid productionSiteId);
    Task<List<ProxyDto>> GetProxiesByProductionSiteAsync(Guid productionSiteId);

    #endregion

    #region Commands

    Task<PrinterDto> CreateAsync(PrinterCreateDto dto);
    Task<PrinterDto> UpdateAsync(Guid id, PrinterUpdateDto dto);

    #endregion
}