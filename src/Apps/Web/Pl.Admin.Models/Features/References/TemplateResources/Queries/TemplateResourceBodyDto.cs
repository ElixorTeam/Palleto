namespace Pl.Admin.Models.Features.References.TemplateResources.Queries;

public class TemplateResourceBodyDto
{
    [JsonPropertyName("body")]
    public required string Body { get; set; } = string.Empty;
}