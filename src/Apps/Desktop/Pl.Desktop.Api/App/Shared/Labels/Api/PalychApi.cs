using Pl.Desktop.Api.App.Shared.Labels.Api.Pallet.Input;
using Pl.Desktop.Api.App.Shared.Labels.Api.Pallet.Output;
using Refit;

namespace Pl.Desktop.Api.App.Shared.Labels.Api;

internal interface IPalychApi
{
    [Post("/ExchangeVesovayaPalletCard")]
    Task<PalletResponseDto> CreatePallet([Body] PalletCreateApiDto dto);

    [Post("/ExchangeVesovayaPalletCardStatus")]
    Task<PalletDeleteWrapperMsg> Delete([Body] PalletDeleteWrapper dto);
}