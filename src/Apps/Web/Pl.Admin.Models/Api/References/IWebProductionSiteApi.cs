using Pl.Admin.Models.Features.References.ProductionSites.Commands;
using Pl.Admin.Models.Features.References.ProductionSites.Queries;

namespace Pl.Admin.Models.Api.References;

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

    [Delete("/production-sites/{uid}")]
    Task DeleteProductionSite(Guid uid);

    [Post("/production-sites")]
    Task<ProductionSiteDto> CreateProductionSite([Body] ProductionSiteCreateDto createDto);

    [Put("/production-sites/{uid}")]
    Task<ProductionSiteDto> UpdateProductionSite(Guid uid, [Body] ProductionSiteUpdateDto updateDto);

    #endregion
}