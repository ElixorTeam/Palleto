using Pl.Desktop.Api.App.Features.Plu.Common;
using Pl.Desktop.Models.Features.Labels.Input;
using Pl.Desktop.Models.Features.Labels.Output;
using Pl.Desktop.Models.Features.Plus.Piece.Output;
using Pl.Desktop.Models.Features.Plus.Weight.Output;

namespace Pl.Desktop.Api.App.Features.Plu;

[ApiController]
[Authorize]
[Route(ApiEndpoints.Plu)]
public sealed class PluWeightController(IPluWeightService pluWeightService, IPluPieceService pluPieceService) : ControllerBase
{
    #region Queries

    [Authorize(PolicyEnum.Pc)]
    [HttpGet("piece")]
    public Task<PluPieceDto[]> GetAllPieceByArm() =>
        pluPieceService.GetAllPieceAsync();

    [Authorize(PolicyEnum.Tablet)]
    [HttpGet("weight")]
    public Task<PluWeightDto[]> GetAllWeightByArm() =>
        pluWeightService.GetAllWeightAsync();

    #endregion

    #region Commands

    [Authorize(PolicyEnum.Tablet)]
    [HttpPost("weight/{pluId:guid}/label")]
    public Task<PrintSuccessDto> GenerateLabel(Guid pluId, [FromBody] CreateWeightLabelDto dto) =>
        pluWeightService.GenerateLabel(pluId, dto);

    #endregion
}