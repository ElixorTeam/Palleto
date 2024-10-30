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
            Brand = new(plu.Brand.Id, plu.Brand.Name),
            ShelfLifeDays = (ushort)plu.ShelfLifeDays,
            Template = plu.TemplateId != null ? new(plu.Template.Id, plu.Template.Name) : null,
            StorageMethod = plu.StorageMethod,
            Name = plu.Name,
            FullName = plu.FullName,
            Description = plu.Description,
            Ean13 = plu.Ean13,
            Gtin = plu.Ean13,
            Clip = new(plu.Clip.Id, plu.Clip.Name),
            Bundle = new(plu.Bundle.Id, plu.Bundle.Name),
            CreateDt = plu.CreateDt,
            ChangeDt = plu.ChangeDt
        };
}