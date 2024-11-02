using Pl.Admin.Api.App.Features.References.Templates.Impl.Models;
using Pl.Admin.Api.App.Shared.Validators.Api.Models;
using Pl.Database.Entities.Zpl.Templates;
using Pl.Admin.Models.Features.References.Template.Queries;

namespace Pl.Admin.Api.App.Features.References.Templates.Impl.Expressions;

internal static class TemplateExpressions
{
    public static Expression<Func<TemplateEntity, ProxyDto>> ToProxy =>
        template => ProxyUtils.Template(template);

    public static Expression<Func<TemplateEntity, TemplateDto>> ToDto =>
        template => new()
        {
            Id = template.Id,
            Name = template.Name,
            IsWeight = template.IsWeight,
            Width = (ushort)template.Width,
            Height = (ushort)template.Height,
            Rotate = (ushort)template.Rotate,
            CreateDt = template.CreateDt,
            ChangeDt = template.ChangeDt
        };

    public static List<PredicateField<TemplateEntity>> GetUqPredicates(UqTemplateProperties uq) =>
    [
        new(i => i.Name == uq.Name, "Name"),
    ];
}