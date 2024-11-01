using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Models.Api;

public interface IDesktopPalletApi
{
    #region Queries

    [Get("/pallets/{number}")]
    Task<PalletDto[]> GetPalletByNumber(uint number);

    [Get("/pallets")]
    Task<PalletDto[]> GetPalletsByArm(DateTime? startDt, DateTime? endDt);

    [Get("/pallets/{palletId}/labels")]
    Task<LabelDto[]> GetPalletLabels(Guid palletId);

    #endregion

    #region Commands

    [Delete("/pallets/{palletId}")]
    Task DeletePallet(Guid palletId);

    [Post("/pallets")]
    Task<PalletDto> CreatePiecePallet([Body] PalletPieceCreateDto createDto);

    #endregion
}