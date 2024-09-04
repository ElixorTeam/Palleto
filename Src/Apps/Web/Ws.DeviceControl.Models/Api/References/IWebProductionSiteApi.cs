using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands.Create;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands.Update;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Queries;

namespace Ws.DeviceControl.Models.Api.References;

public interface IWebProductionSiteApi
{
    #region Queries

    [Get("/production-sites/proxy")]
    Task<ProxyDto[]> GetProxyProductionSites();

    [Get("/production-sites/user-proxy")]
    Task<ProxyDto> GetUserProxyProductionSite();

    [Get("/production-sites")]
    Task<ProductionSiteDto[]> GetProductionSites();

    [Get("/production-sites/{uid}")]
    Task<ProductionSiteDto> GetProductionSiteByUid(Guid uid);

    #endregion

    #region Commands

    [Post("/production-sites/{uid}/delete")]
    Task DeleteProductionSite(Guid uid);

    [Post("/production-sites")]
    Task<ProductionSiteDto> CreateProductionSite([Body] ProductionSiteCreateDto createDto);

    [Post("/production-sites/{uid}")]
    Task<ProductionSiteDto> UpdateProductionSite(Guid uid, [Body] ProductionSiteUpdateDto updateDto);

    #endregion
}