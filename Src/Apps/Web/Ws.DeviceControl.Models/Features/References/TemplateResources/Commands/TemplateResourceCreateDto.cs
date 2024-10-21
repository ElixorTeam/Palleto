namespace Ws.DeviceControl.Models.Features.References.TemplateResources.Commands;

public sealed record TemplateResourceCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ZplResourceType>))]
    public ZplResourceType Type { get; set; }
}

public sealed class TemplateResourceCreateValidator : AbstractValidator<TemplateResourceCreateDto>
{
    public TemplateResourceCreateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(64)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Body)
            .NotEmpty().MaximumLength(8000)
            .WithName(wsDataLocalizer["ColTemplate"]);
    }
}