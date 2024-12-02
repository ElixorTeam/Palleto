using Pl.Admin.Api.App.Features.References.Warehouses.Common;
using Pl.Admin.Models.Features.References.Warehouses.Commands;
using Pl.Admin.Models.Features.References.Warehouses.Queries;

namespace Pl.Admin.Api.App.Features.References.Warehouses;

[ApiController]
[Route(ApiEndpoints.Warehouses)]
public sealed class WarehouseController(IWarehouseService warehouseService)
{
    #region Queries

    [Authorize(PolicyEnum.Support)]
    [HttpGet]
    public Task<WarehouseDto[]> GetAllByProdSite([FromQuery(Name = "productionSite")] Guid prodSiteId) =>
        warehouseService.GetAllByProdSiteAsync(prodSiteId);

    [Authorize(PolicyEnum.Support)]
    [HttpGet("{id:guid}")]
    public Task<WarehouseDto> GetById([FromRoute] Guid id) => warehouseService.GetByIdAsync(id);

    [HttpGet("proxy")]
    public Task<ProxyDto[]> GetProxiesByProdSite([FromQuery(Name = "productionSite")] Guid prodSiteId) =>
        warehouseService.GetProxiesByProdSiteAsync(prodSiteId);

    #endregion

    #region Commands

    [Authorize(PolicyEnum.Admin)]
    [HttpPost]
    public Task<WarehouseDto> Create([FromBody] WarehouseCreateDto dto) =>
        warehouseService.CreateAsync(dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpPut("{id:guid}")]
    public Task<WarehouseDto> Update([FromRoute] Guid id, [FromBody] WarehouseUpdateDto dto) =>
        warehouseService.UpdateAsync(id, dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpDelete("{id:guid}")]
    public Task Delete([FromRoute] Guid id) =>
        warehouseService.DeleteAsync(id);

    #endregion
}