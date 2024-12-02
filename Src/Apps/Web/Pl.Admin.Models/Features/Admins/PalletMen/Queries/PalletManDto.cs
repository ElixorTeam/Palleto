using Pl.Shared.Json.Converters;

namespace Pl.Admin.Models.Features.Admins.PalletMen.Queries;

public sealed record PalletManDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("id1C")]
    public required Guid Id1C { get; init; }

    [JsonPropertyName("fio")]
    [JsonConverter(typeof(FioJsonConverter))]
    public required Fio Fio { get; init; }

    [JsonPropertyName("password")]
    public required string Password { get; init; }

    [JsonPropertyName("warehouse")]
    public required ProxyDto Warehouse { get; init; }

    [JsonPropertyName("productionSite")]
    public required ProxyDto ProductionSite { get; init; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; init; }

    [JsonPropertyName("changeDt")]
    public required DateTime ChangeDt { get; init; }
}