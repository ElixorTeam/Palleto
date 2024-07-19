using Ws.Database.EntityFramework.Entities.Ref.Warehouses;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Warehouses.Impl.Expressions;

public static class WarehouseExpressions
{
    public static Expression<Func<WarehouseEntity, WarehouseDto>> ToDto =>
        warehouse => new()
        {
            Id = warehouse.Id,
            Id1C = warehouse.Uid1C,
            Name = warehouse.Name,
            CreateDt = warehouse.CreateDt,
            ChangeDt = warehouse.ChangeDt
        };

    public static Expression<Func<WarehouseEntity, ProxyDto>> ToProxy =>
        warehouse => new()
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
        };
}