namespace Pl.Admin.Models.Features.Devices.Arms.Queries;

public sealed record AnalyticDto(
    [property: JsonPropertyName("date")] DateTime Date,
    [property: JsonPropertyName("count")] uint Count
);