using Pl.Admin.Api.App.Features.Devices.Arms.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api.Models;
using Pl.Database.Entities.Ref1C.Plus;
using Pl.Admin.Models.Features.Devices.Arms.Queries;
using Pl.Database.Entities.Ref.Arms;

namespace Pl.Admin.Api.App.Features.Devices.Arms.Impl.Expressions;

public static class ArmExpressions
{
    public static Expression<Func<ArmEntity, ArmDto>> ToDto =>
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

    public static List<PredicateField<ArmEntity>> GetUqPredicates(UqArmProperties uqArmProperties) =>
    [
        new(i => i.Name == uqArmProperties.Name, "Name"),
        new(i => i.Number == uqArmProperties.Number, "Number"),
        new(i => i.SystemKey == uqArmProperties.SystemKey, "SystemKey"),
    ];
}