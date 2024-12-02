namespace Pl.Admin.Client.Source.Shared.Auth.Settings;

public class OidcSettings
{
    [JsonPropertyName("RequireHttpsMetadata")]
    public bool RequireHttpsMetadata { get; set; }

    [JsonPropertyName("Scheme")]
    public string Scheme { get; set; } = string.Empty;

    [JsonPropertyName("Realm")]
    public string Realm { get; set; } = string.Empty;

    [JsonPropertyName("ClientId")]
    public string ClientId { get; set; } = string.Empty;

    [JsonPropertyName("Authority")]
    public string Authority { get; set; } = string.Empty;

    [JsonPropertyName("ClientSecret")]
    public string ClientSecret { get; set; } = string.Empty;

    public string AuthorityFull => $"{Authority}/realms/{Realm}";
}