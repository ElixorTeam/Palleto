using Ws.Shared.Json.Converters;

namespace Ws.Desktop.Models.Features.PalletMen;

public sealed record PalletManDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("fio")]
    [JsonConverter(typeof(FioJsonConverter))]
    public required Fio Fio { get; init; }

    [JsonPropertyName("password")]
    public required string Password { get; init; }
}