using Pl.Admin.Models.Features.References.Template.Commands;
using Pl.Admin.Models.Features.References.Template.Queries;

namespace Pl.Admin.Models.Features.References.Template;

public static class TemplateMapper
{
    public static TemplateUpdateDto DtoToUpdateDto(TemplateDto item)
    {
        return new()
        {
            Name = item.Name,
            Width = (short)item.Width,
            Height = (short)item.Height,
            Rotate = (short)item.Rotate,
            Body = string.Empty
        };
    }
}