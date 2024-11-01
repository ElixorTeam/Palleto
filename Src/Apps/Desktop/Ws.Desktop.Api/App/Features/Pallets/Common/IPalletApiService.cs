using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Api.App.Features.Pallets.Common;

public interface IPalletApiService
{
    #region Queries

    public Task<PalletDto[]> GetByNumberAsync(string number);
    public Task<LabelDto[]> GetAllZplByPalletAsync(Guid palletId);
    public Task<PalletDto[]> GetByDateAsync(DateTime startTime, DateTime endTime);

    #endregion

    #region Commands

    public Task DeleteAsync(Guid id);
    public Task<PalletDto> CreatePiecePalletAsync(PalletPieceCreateDto dto);

    #endregion
}