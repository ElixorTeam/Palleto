using Pl.Database.Entities.Ref1C.Boxes;

namespace Pl.Admin.Api.App.Features.References1C.Boxes.Impl.Expressions;

public static class BoxExpressions
{
    public static Expression<Func<BoxEntity, PackageDto>> ToDto =>
        box => new()
        {
            Id = box.Id,
            Name = box.Name,
            Weight = box.Weight,
            CreateDt = box.CreateDt,
            ChangeDt = box.ChangeDt
        };
}