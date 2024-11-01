using Ws.Desktop.Models.Features.Plus.Piece.Output;

namespace Ws.Desktop.Models.Api;

public interface IDesktopPluPieceApi
{
    #region Queries

    [Get("/plu/piece")]
    Task<PluPieceDto[]> GetPlusPieceByArm();

    #endregion
}