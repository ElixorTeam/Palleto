namespace Ws.Desktop.Models.Shared;

public sealed record ProxyDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("name")] string Name
);