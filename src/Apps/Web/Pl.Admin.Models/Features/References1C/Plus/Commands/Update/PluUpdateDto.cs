namespace Pl.Admin.Models.Features.References1C.Plus.Commands.Update;

public sealed record PluUpdateDto
{
    [JsonPropertyName("templateId")]
    public Guid TemplateId { get; set; }
}