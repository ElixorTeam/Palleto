namespace Pl.Desktop.Api.App.Shared.Labels.Settings;

public class PalychSettings
{
    [JsonPropertyName("Login")]
    public string Login { get; set; } = string.Empty;

    [JsonPropertyName("Password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("Url")]
    public string Url { get; set; } = string.Empty;

    public string AuthorizationToken => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Login}:{Password}"));
}