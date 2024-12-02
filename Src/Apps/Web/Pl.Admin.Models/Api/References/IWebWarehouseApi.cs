using Pl.Admin.Models.Features.References.Warehouses.Commands;
using Pl.Admin.Models.Features.References.Warehouses.Queries;

namespace Pl.Admin.Models.Api.References;

public interface IWebWarehouseApi
{
    #region Queries

    [Get("/warehouses/{uid}")]
    Task<WarehouseDto> GetWarehouseByUid(Guid uid);

    [Get("/warehouses?productionSite={productionSiteUid}")]
    Task<WarehouseDto[]> GetWarehousesByProductionSite(Guid productionSiteUid);

    [Get("/warehouses/proxy?productionSite={productionSiteUid}")]
    Task<ProxyDto[]> GetProxyWarehousesByProductionSite(Guid productionSiteUid);

    #endregion

    #region Commands

    [Delete("/warehouses/{uid}")]
    Task DeleteWarehouse(Guid uid);

    [Post("/warehouses")]
    Task<WarehouseDto> CreateWarehouse([Body] WarehouseCreateDto createDto);

    [Put("/warehouses/{uid}")]
    Task<WarehouseDto> UpdateWarehouse(Guid uid, [Body] WarehouseUpdateDto updateDto);

    #endregion
}