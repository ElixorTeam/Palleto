namespace Pl.Admin.Models.Features.Admins.Users.Queries;

public sealed record UserDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("productionSiteId")]
    public required Guid ProductionSiteId { get; init; }
}