using Ws.Desktop.Models.Features.Plus.Piece.Output;

namespace ScalesDesktop.Source.Features.PalletCreate;

public class PalletCreateModel
{
    public PluPieceDto? Plu { get; set; }
    public NestingDto? Nesting { get; set; }
    public int Count { get; set; } = 1;
    public decimal PalletWeight { get; set; }
    public short Kneading { get; set; } = 1;
    public DateTime? CreateDt { get; set; } = DateTime.Now;
}