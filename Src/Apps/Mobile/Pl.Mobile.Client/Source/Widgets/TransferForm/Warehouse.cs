namespace Pl.Mobile.Client.Source.Widgets.TransferForm;

public record Warehouse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
