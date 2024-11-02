using Microsoft.AspNetCore.Components;
using Pl.Tablet.Models.Features.Pallets.Output;

namespace Pl.Tablet.Client.Source.Features.CreatePalletDialog;

public record CreateDialogProps
{
    public string DocumentNumber { get; init; } = string.Empty;
    public EventCallback<PalletDto> OnPalletCreated { get; init; }
}