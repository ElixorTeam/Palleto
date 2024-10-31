using Ws.DeviceControl.Models.Features.Devices.Printers.Commands;
using Ws.DeviceControl.Models.Features.Devices.Printers.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers.Common;

public interface IPrinterService :
    IDeleteById,
    IGetById<PrinterDto>,
    IGetByProdSite<PrinterDto>,
    IGetProxiesByProdSite
{
    #region Commands

    Task<PrinterDto> CreateAsync(PrinterCreateDto dto);
    Task<PrinterDto> UpdateAsync(Guid id, PrinterUpdateDto dto);

    #endregion
}