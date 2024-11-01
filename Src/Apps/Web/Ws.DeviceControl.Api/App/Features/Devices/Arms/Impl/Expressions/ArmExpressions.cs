using Ws.Database.Entities.Ref.Lines;
using Ws.Database.Entities.Ref1C.Plus;
using Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;
using Ws.DeviceControl.Models.Features.Devices.Arms.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Impl.Expressions;

public static class ArmExpressions
{
    public static Expression<Func<LineEntity, ArmDto>> ToDto =>
        arm => new()
        {
            Id = arm.Id,
            Name = arm.Name,
            Version = arm.Version,
            Type = arm.Type,
            Number = arm.Number,
            Counter = arm.Counter,
            SystemKey = arm.SystemKey,
            Printer = ProxyUtils.Printer(arm.Printer),
            Warehouse = ProxyUtils.Warehouse(arm.Warehouse),
            ProductionSite = ProxyUtils.ProductionSite(arm.Warehouse.ProductionSite),
            CreateDt = arm.CreateDt,
            ChangeDt = arm.ChangeDt
        };

    public static Expression<Func<PluEntity, PluArmDto>> ToPluDto(Guid[] plusId) =>
        plu => new()
        {
            Id = plu.Id,
            Name = plu.Name,
            Number = (ushort)plu.Number,
            IsWeight = plu.IsWeight,
            Brand = plu.Brand.Name,
            IsActive = plusId.Contains(plu.Id)
        };

    public static List<PredicateField<LineEntity>> GetUqPredicates(UqArmProperties uqArmProperties) =>
    [
        new(i => i.Name == uqArmProperties.Name, "Name"),
        new(i => i.Number == uqArmProperties.Number, "Number"),
        new(i => i.SystemKey == uqArmProperties.SystemKey, "SystemKey"),
    ];
}