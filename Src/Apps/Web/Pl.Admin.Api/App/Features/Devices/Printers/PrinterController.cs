using Pl.Admin.Api.App.Features.Devices.Printers.Common;
using Pl.Admin.Models.Features.Devices.Printers.Commands;
using Pl.Admin.Models.Features.Devices.Printers.Queries;

namespace Pl.Admin.Api.App.Features.Devices.Printers;

[ApiController]
[Authorize(PolicyEnum.Support)]
[Route(ApiEndpoints.Printers)]
public sealed class PrinterController(IPrinterService printerService)
{
    #region Queries

    [HttpGet("proxy")]
    public Task<ProxyDto[]> GetProxiesByProdSite([FromQuery(Name = "productionSite")] Guid prodSiteId) =>
        printerService.GetProxiesByProdSiteAsync(prodSiteId);

    [HttpGet]
    public Task<PrinterDto[]> GetAllByProdSite([FromQuery(Name = "productionSite")] Guid prodSiteId) =>
        printerService.GetAllByProdSiteAsync(prodSiteId);

    [HttpGet("{id:guid}")]
    public Task<PrinterDto> GetById([FromRoute] Guid id) =>
        printerService.GetByIdAsync(id);

    #endregion

    #region Commands

    [HttpPost]
    [Authorize(PolicyEnum.SeniorSupport)]
    public Task<PrinterDto> Create([FromBody] PrinterCreateDto dto) =>
        printerService.CreateAsync(dto);

    [HttpPut("{id:guid}")]
    public Task<PrinterDto> Update([FromRoute] Guid id, [FromBody] PrinterUpdateDto dto) =>
        printerService.UpdateAsync(id, dto);

    [HttpDelete("{id:guid}")]
    [Authorize(PolicyEnum.SeniorSupport)]
    public Task Delete([FromRoute] Guid id) =>
        printerService.DeleteAsync(id);

    #endregion
}