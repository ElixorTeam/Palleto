namespace Pl.Tablet.Client.Source.Features.CreatePalletDialog;

public record PluModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Number { get; set; } = string.Empty;
}