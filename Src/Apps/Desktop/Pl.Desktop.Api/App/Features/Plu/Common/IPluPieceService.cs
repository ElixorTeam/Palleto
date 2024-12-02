using Pl.Desktop.Models.Features.Plus.Piece.Output;

namespace Pl.Desktop.Api.App.Features.Plu.Common;

public interface IPluPieceService
{
    public Task<PluPieceDto[]> GetAllPieceAsync();
}