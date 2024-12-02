using Pl.Admin.Api.App.Features.References.Warehouses.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api.Models;
using Pl.Database.Entities.Ref.Warehouses;
using Pl.Admin.Models.Features.References.Warehouses.Queries;

namespace Pl.Admin.Api.App.Features.References.Warehouses.Impl.Expressions;

public static class WarehouseExpressions
{
    public static Expression<Func<WarehouseEntity, WarehouseDto>> ToDto =>
        warehouse => new()
        {
            Id = warehouse.Id,
            Id1C = warehouse.Uid1C,
            Name = warehouse.Name,
            ProductionSite = ProxyUtils.ProductionSite(warehouse.ProductionSite),
            CreateDt = warehouse.CreateDt,
            ChangeDt = warehouse.ChangeDt
        };

    public static Expression<Func<WarehouseEntity, ProxyDto>> ToProxy =>
        warehouse => ProxyUtils.Warehouse(warehouse);

    public static List<PredicateField<WarehouseEntity>> GetUqPredicates(UqWarehousesProperties uqWarehouseProperties) =>
    [
        new(i => i.Name == uqWarehouseProperties.Name, "Name"),
        new(i => i.Uid1C == uqWarehouseProperties.Uid1C, "Uid1C"),
    ];
}