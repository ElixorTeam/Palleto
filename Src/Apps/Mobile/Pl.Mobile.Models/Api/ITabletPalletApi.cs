using Pl.Mobile.Models.Features.Pallets;

namespace Pl.Mobile.Models.Api;

public interface ITabletPalletApi
{
    [Post("/pallets")]
    Task MovePallets([Body] PalletsMoveDto pallet);
}