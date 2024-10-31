namespace Ws.DeviceControl.Models.Features.Print.Pallets;

public class PluPalletDto
{
    [JsonPropertyName("Plu")]
    public required ProxyDto Plu { get; init; }

    [JsonPropertyName("boxCount")]
    public required ushort BoxCount { get; init; }

    [JsonPropertyName("bundleCount")]
    public required ushort BundleCount { get; init; }

    [JsonPropertyName("weightBrutto")]
    public required decimal WeightBrutto { get; init; }

    [JsonPropertyName("weightNet")]
    public required decimal WeightNet { get; init; }

    [JsonPropertyName("kneading")]
    public required ushort Kneading { get; init; }
}