﻿using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Labels.Service.Features.PrintLabel.Types.Piece.Dto;

public record LabelPiecePalletDto
{
    public required PalletManEntity PalletMan { get; init; }
    public required Decimal Weight { get; init; }
    public required DateTime ProductDt { get; init; }
    public required LineEntity Line { get; init; }
    public required PluNestingEntity Nesting { get; init; }
    public required string Template { get; init; }
    public required short Kneading { get; init; }
    public required DateTime ExpirationDt { get; init; }
}