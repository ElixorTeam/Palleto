namespace Pl.Admin.Models.Features.References.TemplateResources.Commands;

public sealed record ZplResourceCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ZplResourceType>))]
    public ZplResourceType Type { get; set; }
}

public sealed class ZplResourceCreateValidator : AbstractValidator<ZplResourceCreateDto>
{
    public ZplResourceCreateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(64)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Body)
            .NotEmpty().MaximumLength(8000)
            .WithName(wsDataLocalizer["ColTemplate"]);
    }
}