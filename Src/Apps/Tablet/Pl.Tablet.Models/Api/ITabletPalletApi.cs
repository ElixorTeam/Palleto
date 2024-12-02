using Pl.Tablet.Models.Features.Pallets.Input;
using Pl.Tablet.Models.Features.Pallets.Output;

namespace Pl.Tablet.Models.Api;

public interface ITabletPalletApi
{
    [Post("/pallets")]
    Task<PalletDto> CreatePallet([Body] PalletCreateDto pallet);
}