namespace Pl.Admin.Client.Source.Shared.Auth.Options;

public class OidcOptions
{
    [JsonPropertyName("RequireHttpsMetadata")]
    public bool RequireHttpsMetadata { get; init; }

    [JsonPropertyName("Scheme")]
    public string Scheme { get; init; } = string.Empty;

    [JsonPropertyName("Realm")]
    public string Realm { get; init; } = string.Empty;

    [JsonPropertyName("ClientId")]
    public string ClientId { get; init; } = string.Empty;

    [JsonPropertyName("Authority")]
    public string Authority { get; init; } = string.Empty;

    [JsonPropertyName("ClientSecret")]
    public string ClientSecret { get; init; } = string.Empty;

    public string AuthorityFull => $"{Authority}/realms/{Realm}";
}