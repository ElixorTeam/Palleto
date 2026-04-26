namespace Pl.Admin.Models.Features.Database;

public sealed record DataBaseTableDto
{
    [JsonPropertyName("rows")]
    public required int Rows { get; init; }

    [JsonPropertyName("usedMb")]
    public required int UsedMb { get; init; }

    [JsonPropertyName("table")]
    public required string Table { get; init; }

    [JsonPropertyName("schema")]
    public required string Schema { get; init; }

    [JsonPropertyName("fileName")]
    public required string FileName { get; init; }
}