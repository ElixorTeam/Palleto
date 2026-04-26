using Pl.Desktop.Models.Shared;
using Pl.Shared.Enums;

namespace Pl.Desktop.Models.Features.Arms.Output;

public sealed record ArmDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ArmType>))]
    public required ArmType Type { get; init; }

    [JsonPropertyName("systemKey")]
    public required Guid SystemKey { get; init; }

    [JsonPropertyName("counter")]
    public required uint Counter { get; init; }

    [JsonPropertyName("warehouse")]
    public required ProxyDto Warehouse { get; init; }

    [JsonPropertyName("printer")]
    public required PrinterDto Printer { get; init; }
}