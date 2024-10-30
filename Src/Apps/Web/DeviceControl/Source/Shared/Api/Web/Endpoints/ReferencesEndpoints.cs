using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Queries;
using Ws.DeviceControl.Models.Features.References.Warehouses.Queries;
// ReSharper disable ClassNeverInstantiated.Global

namespace DeviceControl.Source.Shared.Api.Web.Endpoints;

public class ReferencesEndpoints(IWebApi webApi)
{
    # region Production Site

    public ParameterlessEndpoint<ProductionSiteDto[]> ProductionSitesEndpoint { get; } = new(
        webApi.GetProductionSites,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddProductionSite(ProductionSiteDto productionSite)
    {
        ProductionSitesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Prepend(productionSite).ToArray());
        AddProxyProductionSite(new(productionSite.Id, productionSite.Name));
    }

    public void UpdateProductionSite(ProductionSiteDto productionSite)
    {
        ProductionSitesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemBy(productionSite, p => p.Id == productionSite.Id).ToArray());
        UpdateProxyProductionSite(new(productionSite.Id, productionSite.Name));
    }

    public void DeleteProductionSite(Guid productionSiteId)
    {
        ProductionSitesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != productionSiteId).ToArray());
        DeleteProxyProductionSite(productionSiteId);
    }

    # endregion

    # region Proxy Production Site

    public ParameterlessEndpoint<ProxyDto> ProxyUserProductionSiteEndpoint { get; } = new(
        webApi.GetUserProxyProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public ParameterlessEndpoint<ProxyDto[]> ProxyProductionSiteEndpoint { get; } = new(
        webApi.GetProxyProductionSites,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddProxyProductionSite(ProxyDto productionSite) =>
        ProxyProductionSiteEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Prepend(productionSite).ToArray());

    public void UpdateProxyProductionSite(ProxyDto productionSite) =>
        ProxyProductionSiteEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemBy(productionSite, p => p.Id == productionSite.Id).ToArray());

    public void DeleteProxyProductionSite(Guid productionSiteId) =>
        ProxyProductionSiteEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != productionSiteId).ToArray());

    # endregion

    # region Warehouse

    public Endpoint<Guid, WarehouseDto[]> WarehousesEndpoint { get; } = new(
        webApi.GetWarehousesByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddWarehouse(Guid productionSiteId, WarehouseDto warehouse)
    {
        WarehousesEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Prepend(warehouse).ToArray());
        AddProxyWarehouse(productionSiteId, new(warehouse.Id, warehouse.Name));
    }

    public void UpdateWarehouse(Guid productionSiteId, WarehouseDto warehouse)
    {
        WarehousesEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemBy(warehouse, p => p.Id == warehouse.Id).ToArray());
        UpdateProxyWarehouse(productionSiteId, new(warehouse.Id, warehouse.Name));
    }

    public void DeleteWarehouse(Guid productionSiteId, Guid warehouseId)
    {
        WarehousesEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != warehouseId).ToArray());
        DeleteProxyWarehouse(productionSiteId, warehouseId);
    }

    # endregion

    # region Proxy Warehouse

    public Endpoint<Guid, ProxyDto[]> ProxyWarehousesEndpoint { get; } = new(
        webApi.GetProxyWarehousesByProductionSite,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddProxyWarehouse(Guid productionSiteId, ProxyDto warehouse) =>
        ProxyWarehousesEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Prepend(warehouse).ToArray());

    public void UpdateProxyWarehouse(Guid productionSiteId, ProxyDto warehouse) =>
        ProxyWarehousesEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemBy(warehouse, p => p.Id == warehouse.Id).ToArray());

    public void DeleteProxyWarehouse(Guid productionSiteId, Guid warehouseId) =>
        ProxyWarehousesEndpoint.UpdateQueryData(productionSiteId, query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != warehouseId).ToArray());

    # endregion
}