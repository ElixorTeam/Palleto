using Pl.Admin.Api.App.Features.References.TemplateResources.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api.Models;
using Pl.Database.Entities.Zpl.ZplResources;
using Pl.Admin.Models.Features.References.TemplateResources.Queries;

namespace Pl.Admin.Api.App.Features.References.TemplateResources.Impl.Expressions;

internal static class ZplResourceExpressions
{
    public static Expression<Func<ZplResourceEntity, TemplateResourceDto>> ToDto =>
        zpl => new()
        {
            Id = zpl.Id,
            Name = zpl.Name,
            Type = zpl.Type,
            CreateDt = zpl.CreateDt,
            ChangeDt = zpl.ChangeDt
        };

    public static List<PredicateField<ZplResourceEntity>> GetUqPredicates(UqZplResourceProperties uq) =>
    [
        new(i => i.Name == uq.Name, "Name"),
    ];
}