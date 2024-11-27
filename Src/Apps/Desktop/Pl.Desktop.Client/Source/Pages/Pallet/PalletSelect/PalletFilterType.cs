namespace Pl.Desktop.Client.Source.Pages.Pallet.PalletSelect;

public enum PalletFilterType
{
    [Description("PalletFilterNotShipped")]
    NotShipped,
    [Description("PalletFilterShipped")]
    Shipped,
    [Description("PalletFilterDeleted")]
    Deleted,
    [Description("PalletFilterAll")]
    All
}