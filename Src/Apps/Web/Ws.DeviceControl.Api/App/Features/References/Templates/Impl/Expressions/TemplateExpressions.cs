using Ws.Database.Entities.Zpl.Templates;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Models;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;
using Ws.DeviceControl.Models.Features.References.Template.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Expressions;

internal static class TemplateExpressions
{
    public static Expression<Func<TemplateEntity, ProxyDto>> ToProxy =>
        template => new(template.Id, template.Name);

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