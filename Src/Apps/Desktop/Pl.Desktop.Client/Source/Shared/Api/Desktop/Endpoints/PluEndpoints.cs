using Pl.Desktop.Models.Features.Plus.Piece.Output;
using Pl.Desktop.Models.Features.Plus.Weight.Output;

namespace Pl.Desktop.Client.Source.Shared.Api.Desktop.Endpoints;

public class PluEndpoints(IDesktopApi desktopApi)
{
    public ParameterlessEndpoint<PluWeightDto[]> WeightPlusEndpoint { get; } = new(
         desktopApi.GetPlusWeightByArm,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public ParameterlessEndpoint<PluPieceDto[]> PiecePlusEndpoint { get; } = new(
        desktopApi.GetPlusPieceByArm,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });
}