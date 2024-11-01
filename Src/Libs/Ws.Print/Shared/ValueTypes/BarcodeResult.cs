namespace Ws.Print.Shared.ValueTypes;

public sealed record BarcodeResult(string Barcode)
{
    // ReSharper disable once UnusedMember.Local
    private string Barcode { get; } = Barcode;

    public readonly string Clean = string.Concat(Barcode.Where(char.IsDigit));
    public readonly string Zpl = string.Concat(Barcode.Where("0123456789#".Contains));
    public readonly string Friendly = string.Concat(Barcode.Where("0123456789()".Contains));
}