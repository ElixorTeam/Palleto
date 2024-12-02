using Pl.Mobile.Models.Features.Warehouses;

namespace Pl.Mobile.Models.Api;

public interface ITabletWarehouseApi
{
    [Get("/warehouses")]
    Task<List<WarehouseDto>> GetWarehouses();
}