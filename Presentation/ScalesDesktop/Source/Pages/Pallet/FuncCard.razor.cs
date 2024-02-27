using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Localization;

namespace ScalesDesktop.Source.Pages.Pallet;

public sealed partial class FuncCard : ComponentBase
{
    [Inject] private IStringLocalizer<Resources> PalletLocalizer { get; set; } = null!;
}