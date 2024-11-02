namespace Pl.Admin.Models.Features.Devices.Arms.Commands;

public sealed record ArmUpdateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ArmType>))]
    public ArmType Type { get; set; }

    [JsonPropertyName("number")]
    public int Number { get; set; }

    [JsonPropertyName("counter")]
    public int Counter { get; set; }

    [JsonPropertyName("systemKey")]
    public Guid SystemKey { get; set; }

    [JsonPropertyName("printerId")]
    public Guid PrinterId { get; set; }

    [JsonPropertyName("warehouseId")]
    public Guid WarehouseId { get; set; }
}

public sealed class ArmUpdateValidator : AbstractValidator<ArmUpdateDto>
{
    public ArmUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(32)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Number)
            .GreaterThan(10000).LessThan(100000)
            .WithName(wsDataLocalizer["ColNumber"]);

        RuleFor(item => item.SystemKey)
            .NotEmpty()
            .WithName(wsDataLocalizer["ColSystemKey"]);

        RuleFor(item => item.Counter)
            .GreaterThanOrEqualTo(0).LessThanOrEqualTo(1000000)
            .WithName(wsDataLocalizer["ColCounter"]);

        RuleFor(item => item.Type).IsInEnum().WithName(wsDataLocalizer["ColType"]);
        RuleFor(item => item.PrinterId).NotEmpty().WithName(wsDataLocalizer["ColPrinter"]);
        RuleFor(item => item.WarehouseId).NotEmpty().WithName(wsDataLocalizer["ColWarehouse"]);
    }
}