using Phetch.Core;
using Pl.Mobile.Models;
using Pl.Mobile.Models.Features.Warehouses;

namespace Pl.Mobile.Client.Source.Shared.Api.Mobile.Endpoints;

public class WarehousesEndpoints(IMobileApi mobileApi)
{
    public ParameterlessEndpoint<List<WarehouseDto>> WarehousesEndpoint { get; } = new(
        mobileApi.GetWarehouses,
        options: new() { DefaultStaleTime = TimeSpan.FromHours(1) });
}
