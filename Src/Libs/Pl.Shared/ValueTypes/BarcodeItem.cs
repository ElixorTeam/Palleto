namespace Pl.Shared.ValueTypes;

public record BarcodeItem
{
    public string Property { get; set; } = string.Empty;
    public string Format { get; set; } = string.Empty;
}