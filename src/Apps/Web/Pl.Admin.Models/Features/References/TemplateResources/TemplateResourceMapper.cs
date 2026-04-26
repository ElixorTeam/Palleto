using Pl.Admin.Models.Features.References.TemplateResources.Commands;
using Pl.Admin.Models.Features.References.TemplateResources.Queries;

namespace Pl.Admin.Models.Features.References.TemplateResources;

public static class TemplateResourceMapper
{
    public static ZplResourceUpdateDto DtoToUpdateDto(TemplateResourceDto item)
    {
        return new()
        {
            Name = item.Name,
            Body = string.Empty
        };
    }
}