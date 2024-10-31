using Ws.DeviceControl.Api.App.Features.Devices.Printers.Common;
using Ws.DeviceControl.Models.Features.Devices.Printers.Commands;
using Ws.DeviceControl.Models.Features.Devices.Printers.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Printers;

[ApiController]
[Authorize(PolicyEnum.Support)]
[Route(ApiEndpoints.Printers)]
public sealed class PrinterController(IPrinterService printerService)
{
    #region Queries

    [HttpGet("proxy")]
    public Task<ProxyDto[]> GetProxiesByProdSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        printerService.GetProxiesByProdSiteAsync(productionSiteId);

    [HttpGet]
    public Task<PrinterDto[]> GetAllByProductionSite([FromQuery(Name = "productionSite")] Guid productionSiteId) =>
        printerService.GetAllByProdSiteAsync(productionSiteId);

    [HttpGet("{id:guid}")]
    public Task<PrinterDto> GetById([FromRoute] Guid id) => printerService.GetByIdAsync(id);

    #endregion

    #region Commands

    [HttpPost]
    public Task<PrinterDto> Create([FromBody] PrinterCreateDto dto) =>
        printerService.CreateAsync(dto);

    [HttpPut("{id:guid}")]
    public Task<PrinterDto> Update([FromRoute] Guid id, [FromBody] PrinterUpdateDto dto) =>
        printerService.UpdateAsync(id, dto);

    [HttpDelete("{id:guid}")]
    public Task Delete([FromRoute] Guid id) => printerService.DeleteAsync(id);

    #endregion
}