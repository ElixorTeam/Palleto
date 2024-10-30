namespace Ws.DeviceControl.Models.Shared;

public sealed record ProxyDto(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("name")] string Name
);