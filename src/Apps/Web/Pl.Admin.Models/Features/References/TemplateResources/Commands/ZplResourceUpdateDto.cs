namespace Pl.Admin.Models.Features.References.TemplateResources.Commands;

public sealed record ZplResourceUpdateDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public required string Body { get; set; }
}

public sealed class ZplResourceUpdateValidator : AbstractValidator<ZplResourceUpdateDto>
{
    public ZplResourceUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(64)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Body)
            .NotEmpty().MaximumLength(8000)
            .WithName(wsDataLocalizer["ColTemplate"]);
    }
}