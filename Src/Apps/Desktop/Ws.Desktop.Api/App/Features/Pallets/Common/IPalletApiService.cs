using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Api.App.Features.Pallets.Common;

public interface IPalletApiService
{
    #region Queries

    public Task<PalletInfo[]> GetByNumberAsync(string number);
    public Task<LabelInfo[]> GetAllZplByPalletAsync(Guid palletId);
    public Task<PalletInfo[]> GetAllByDateAsync(DateTime startTime, DateTime endTime);

    #endregion

    #region Commands

    public Task DeleteAsync(Guid id);
    public Task<PalletInfo> CreatePiecePalletAsync(PalletPieceCreateDto dto);

    #endregion
}