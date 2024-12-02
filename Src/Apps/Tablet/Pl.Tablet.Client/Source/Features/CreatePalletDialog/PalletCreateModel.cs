namespace Pl.Tablet.Client.Source.Features.CreatePalletDialog;

public class PalletCreateModel {
    public Guid Id { get; } = Guid.NewGuid();
    public List<BatchCreateModel> Batches { get; set; } = [];
    public PluModel DefaultPlu { get; set; } = new();
    public bool Mono { get; set; } = true;
    public string DocumentNumber { get; set; } = string.Empty;
}
