namespace Pl.Admin.Models.Features.References.Template.Commands;

public sealed record TemplateUpdateDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("width")]
    public required short Width { get; set; }

    [JsonPropertyName("height")]
    public required short Height { get; set; }

    [JsonPropertyName("rotate")]
    public required short Rotate { get; set; }

    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;
}

public sealed class TemplateUpdateValidator : AbstractValidator<TemplateUpdateDto>
{
    public TemplateUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(64)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Width)
            .InclusiveBetween((short)1, (short)512)
            .WithName(wsDataLocalizer["ColHeight"]);

        RuleFor(item => item.Height)
            .InclusiveBetween((short)1, (short)512)
            .WithName(wsDataLocalizer["ColHeight"]);

        RuleFor(item => item.Rotate)
            .Must(value => value is 0 or 90)
            .WithName(wsDataLocalizer["ColRotation"]);

        RuleFor(item => item.Body)
            .NotEmpty().MaximumLength(8000)
            .WithName(wsDataLocalizer["ColTemplate"]);
    }
}