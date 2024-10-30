using Ws.Database.Entities.Ref.Warehouses;
using Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;
using Ws.DeviceControl.Models.Features.References.Warehouses.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Expressions;

public static class WarehouseExpressions
{
    public static Expression<Func<WarehouseEntity, WarehouseDto>> ToDto =>
        warehouse => new()
        {
            Id = warehouse.Id,
            Id1C = warehouse.Uid1C,
            Name = warehouse.Name,
            ProductionSite = new(warehouse.ProductionSite.Id, warehouse.ProductionSite.Name),
            CreateDt = warehouse.CreateDt,
            ChangeDt = warehouse.ChangeDt
        };

    public static Expression<Func<WarehouseEntity, ProxyDto>> ToProxy =>
        warehouse => new(warehouse.Id, warehouse.Name);

    public static List<PredicateField<WarehouseEntity>> GetUqPredicates(UqWarehousesProperties uqWarehouseProperties) =>
    [
        new(i => i.Name == uqWarehouseProperties.Name, "Name"),
        new(i => i.Uid1C == uqWarehouseProperties.Uid1C, "Uid1C"),
    ];
}