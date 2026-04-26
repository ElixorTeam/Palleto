namespace Pl.Admin.Models.Features.Print.Labels;

public record LabelDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("bundleCount")]
    public required byte BundleCount { get; set; }

    [JsonPropertyName("kneading")]
    public required ushort Kneading { get; set; }

    #region Weight

    [JsonPropertyName("isWeight")]
    public required bool IsWeight { get; set; }

    [JsonPropertyName("weightNet")]
    public required decimal WeightNet { get; set; }

    [JsonPropertyName("weightTare")]
    public required decimal WeightTare { get; set; }

    #endregion

    #region Proxies

    [JsonPropertyName("arm")]
    public required ProxyDto Arm { get; set; }

    [JsonPropertyName("warehouse")]
    public required ProxyDto Warehouse { get; set; }

    [JsonPropertyName("plu")]
    public required ProxyDto? Plu { get; set; }

    [JsonPropertyName("pallet")]
    public required ProxyDto? Pallet { get; set; }

    #endregion

    #region Barcodes

    [JsonPropertyName("barcodeTop")]
    public required string BarcodeTop { get; set; }

    [JsonPropertyName("barcodeBottom")]
    public required string BarcodeBottom { get; set; }

    [JsonPropertyName("barcodeRight")]
    public required string BarcodeRight { get; set; }

    #endregion

    #region Dates

    [JsonPropertyName("productDt")]
    public required DateOnly ProductDt { get; set; }

    [JsonPropertyName("expirationDt")]
    public required DateOnly ExpirationDt { get; set; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; set; }

    #endregion
}