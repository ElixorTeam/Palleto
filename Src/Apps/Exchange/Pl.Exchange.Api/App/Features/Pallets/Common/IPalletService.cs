using Pl.Exchange.Api.App.Features.Pallets.Dto;

namespace Pl.Exchange.Api.App.Features.Pallets.Common;

public interface IPalletService
{
    PalletUpdateStatus Update(PalletUpdateDto dto);
}