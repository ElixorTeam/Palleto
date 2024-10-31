namespace DeviceControl.Source.Pages.Operations.Pallets;

public sealed record OrderedPalletLabel
{
    public required int Number { get; init; }
    public required Guid Id { get; init; }
    public required string Barcode { get; init; }
}