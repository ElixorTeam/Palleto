using System.Text;
using System.Text.Json.Serialization;

namespace Ws.Labels.Service.Api;

public class PalychSettingsModel
{
    [JsonPropertyName("Login")]
    public string Login { get; set; } = string.Empty;

    [JsonPropertyName("Password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("Url")]
    public string Url { get; set; } = string.Empty;

    public string AuthorizationToken => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Login}:{Password}"));
}