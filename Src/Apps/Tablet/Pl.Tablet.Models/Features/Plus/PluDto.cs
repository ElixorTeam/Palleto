namespace Pl.Tablet.Models.Features.Plus;

public sealed class PluDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("number")]
    public required uint Number { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}