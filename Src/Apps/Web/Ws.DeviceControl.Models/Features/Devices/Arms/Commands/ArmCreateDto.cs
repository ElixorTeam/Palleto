namespace Ws.DeviceControl.Models.Features.Devices.Arms.Commands;

public sealed record ArmCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ArmType>))]
    public ArmType Type { get; set; }

    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("systemKey")]
    public Guid SystemKey { get; set; }

    [JsonPropertyName("printerId")]
    public Guid PrinterId { get; set; }

    [JsonPropertyName("warehouse")]
    public Guid WarehouseId { get; set; }
}

public sealed class ArmCreateValidator : AbstractValidator<ArmCreateDto>
{
    public ArmCreateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(32)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Number)
            .GreaterThanOrEqualTo(10000).LessThanOrEqualTo(99999)
            .WithName(wsDataLocalizer["ColNumber"]);

        RuleFor(item => item.SystemKey).NotEmpty().WithName(wsDataLocalizer["ColSystemKey"]);

        RuleFor(item => item.Type).IsInEnum().WithName(wsDataLocalizer["ColType"]);
        RuleFor(item => item.PrinterId).NotEmpty().WithName(wsDataLocalizer["ColPrinter"]);
        RuleFor(item => item.WarehouseId).NotEmpty().WithName(wsDataLocalizer["ColWarehouse"]);
    }
}