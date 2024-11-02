namespace Pl.Admin.Models.Features.References.Template.Commands;

public sealed record TemplateCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("width")]
    public short Width { get; set; }

    [JsonPropertyName("height")]
    public short Height { get; set; }

    [JsonPropertyName("rotate")]
    public short Rotate { get; set; }

    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;

    [JsonPropertyName("isWeight")]
    public bool IsWeight { get; set; }
}

public sealed class TemplateCreateValidator : AbstractValidator<TemplateCreateDto>
{
    public TemplateCreateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(64)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Width)
            .InclusiveBetween((short)1, (short)512)
            .WithName(wsDataLocalizer["ColWidth"]);

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