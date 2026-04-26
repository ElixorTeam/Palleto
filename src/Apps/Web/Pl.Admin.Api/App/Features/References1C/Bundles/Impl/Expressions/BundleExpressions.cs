using Pl.Database.Entities.Ref1C.Bundles;

namespace Pl.Admin.Api.App.Features.References1C.Bundles.Impl.Expressions;

public static class BundleExpressions
{
    public static Expression<Func<BundleEntity, PackageDto>> ToDto =>
        bundle => new()
        {
            Id = bundle.Id,
            Name = bundle.Name,
            Weight = bundle.Weight,
            CreateDt = bundle.CreateDt,
            ChangeDt = bundle.ChangeDt
        };
}