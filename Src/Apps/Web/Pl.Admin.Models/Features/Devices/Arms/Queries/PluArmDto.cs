namespace Pl.Admin.Models.Features.Devices.Arms.Queries;

public sealed record PluArmDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("brand")]
    public required string Brand { get; init; }

    [JsonPropertyName("number")]
    public required ushort Number { get; init; }

    [JsonPropertyName("isWeight")]
    public required bool IsWeight { get; init; }

    [JsonPropertyName("isActive")]
    public required bool IsActive { get; init; }
}