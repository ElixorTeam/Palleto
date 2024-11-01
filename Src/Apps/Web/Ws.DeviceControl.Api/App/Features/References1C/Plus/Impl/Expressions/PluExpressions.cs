using Ws.Database.Entities.Ref1C.Plus;
using Ws.DeviceControl.Models.Features.References1C.Plus.Queries;

namespace Ws.DeviceControl.Api.App.Features.References1C.Plus.Impl.Expressions;

public static class PluExpressions
{
    public static Expression<Func<PluEntity, PluDto>> ToDto =>
        plu => new()
        {
            Id = plu.Id,
            Number = (ushort)plu.Number,
            IsWeight = plu.IsWeight,
            Weight = plu.Weight,
            ShelfLifeDays = (ushort)plu.ShelfLifeDays,

            Clip = ProxyUtils.Clip(plu.Clip),
            Brand = ProxyUtils.Brand(plu.Brand),
            Bundle = ProxyUtils.Bundle(plu.Bundle),
            Template = plu.TemplateId != null ? ProxyUtils.Template(plu.Template) : null,

            StorageMethod = plu.StorageMethod,
            Name = plu.Name,
            FullName = plu.FullName,
            Description = plu.Description,
            Ean13 = plu.Ean13,
            Gtin = plu.Ean13,
            CreateDt = plu.CreateDt,
            ChangeDt = plu.ChangeDt
        };
}