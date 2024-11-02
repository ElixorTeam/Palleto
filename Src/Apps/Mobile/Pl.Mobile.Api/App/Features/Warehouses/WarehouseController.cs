using Pl.Mobile.Api.App.Features.Warehouses.Common;
using Pl.Mobile.Models.Features.Warehouses;

namespace Pl.Mobile.Api.App.Features.Warehouses;

[ApiController]
[Route(ApiEndpoints.Warehouses)]
public sealed class WarehouseController(IWarehouseService warehouseService)
{
    #region Queries

    [HttpGet]
    public List<WarehouseDto> GetAll() => warehouseService.GetAll();

    #endregion
}