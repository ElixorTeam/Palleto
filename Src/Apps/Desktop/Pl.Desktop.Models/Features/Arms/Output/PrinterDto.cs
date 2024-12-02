using TscZebra.Plugin.Abstractions.Enums;

namespace Pl.Desktop.Models.Features.Arms.Output;

public sealed record PrinterDto
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("ip")]
    [JsonConverter(typeof(IpV4AddressJsonConverter))]
    public required IPAddress Ip { get; init; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<PrinterTypes>))]
    public required PrinterTypes Type { get; init; }
}