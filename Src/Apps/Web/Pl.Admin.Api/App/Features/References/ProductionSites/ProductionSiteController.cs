using Pl.Admin.Api.App.Features.References.ProductionSites.Common;
using Pl.Admin.Models.Features.References.ProductionSites.Commands;
using Pl.Admin.Models.Features.References.ProductionSites.Queries;

namespace Pl.Admin.Api.App.Features.References.ProductionSites;

[ApiController]
[Route(ApiEndpoints.ProductionSites)]
public sealed class ProductionSiteController(IProductionSiteService productionSiteService)
{
    #region Queries

    #region Support

    [Authorize(PolicyEnum.Support)]
    [HttpGet]
    public Task<ProductionSiteDto[]> GetAll() =>
        productionSiteService.GetAllAsync();

    [Authorize(PolicyEnum.Support)]
    [HttpGet("{id:guid}")]
    public Task<ProductionSiteDto> GetById([FromRoute] Guid id) =>
        productionSiteService.GetByIdAsync(id);

    #endregion

    [HttpGet("user-proxy")]
    public Task<ProxyDto> GetProxyByUser() =>
        productionSiteService.GetProxyByUserAsync();

    [HttpGet("proxy")]
    public Task<ProxyDto[]> GetProxies() =>
        productionSiteService.GetProxiesAsync();

    #endregion

    #region Commamnds

    [Authorize(PolicyEnum.Admin)]
    [HttpPost]
    public Task<ProductionSiteDto> Create([FromBody] ProductionSiteCreateDto dto) =>
        productionSiteService.CreateAsync(dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpPut("{id:guid}")]
    public Task<ProductionSiteDto> Update([FromRoute] Guid id, [FromBody] ProductionSiteUpdateDto dto) =>
        productionSiteService.UpdateAsync(id, dto);

    [Authorize(PolicyEnum.Admin)]
    [HttpDelete("{id:guid}")]
    public Task Delete([FromRoute] Guid id) =>
        productionSiteService.DeleteAsync(id);

    #endregion
}