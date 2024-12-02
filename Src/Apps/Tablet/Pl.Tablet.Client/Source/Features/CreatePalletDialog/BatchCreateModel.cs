namespace Pl.Tablet.Client.Source.Features.CreatePalletDialog;

public record BatchCreateModel {
    public Guid Id { get; } = Guid.NewGuid();
    public PluModel Plu { get; set; } = new();
    public string Date { get; set; } = string.Empty;
    public decimal Weight { get; set; } = decimal.Zero;
}
