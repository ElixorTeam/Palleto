using TscZebra.Plugin.Abstractions.Enums;

namespace Pl.Admin.Models.Features.Devices.Printers.Queries;

public record PrinterDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("productionSite")]
    public required ProxyDto ProductionSite { get; set; }

    [JsonPropertyName("ip")]
    [JsonConverter(typeof(IpV4AddressJsonConverter))]
    public required IPAddress Ip { get; set; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<PrinterTypes>))]
    public required PrinterTypes Type { get; set; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; set; }

    [JsonPropertyName("changeDt")]
    public required DateTime ChangeDt { get; set; }
}