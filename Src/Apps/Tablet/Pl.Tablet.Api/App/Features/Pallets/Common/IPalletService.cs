using Pl.Tablet.Models.Features.Pallets.Input;
using Pl.Tablet.Models.Features.Pallets.Output;

namespace Pl.Tablet.Api.App.Features.Pallets.Common;

public interface IPalletService
{
    #region Commands

    PalletDto Create(PalletCreateDto palletCreateDto);

    #endregion
}