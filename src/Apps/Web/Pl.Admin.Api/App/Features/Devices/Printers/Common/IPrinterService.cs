using Pl.Admin.Models.Features.Devices.Printers.Commands;
using Pl.Admin.Models.Features.Devices.Printers.Queries;

namespace Pl.Admin.Api.App.Features.Devices.Printers.Common;

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