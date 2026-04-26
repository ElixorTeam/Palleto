using Pl.Desktop.Models.Features.Plus.Piece.Output;

namespace Pl.Desktop.Models.Api;

public interface IDesktopPluPieceApi
{
    #region Queries

    [Get("/plu/piece")]
    Task<PluPieceDto[]> GetPlusPieceByArm();

    #endregion
}