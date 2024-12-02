using Pl.Desktop.Api.App.Features.Pallets.Common;
using Pl.Desktop.Models.Features.Pallets.Input;
using Pl.Desktop.Models.Features.Pallets.Output;

namespace Pl.Desktop.Api.App.Features.Pallets;

[ApiController]
[Authorize(PolicyEnum.Pc)]
[Route(ApiEndpoints.Pallets)]
public sealed class PalletController(IPalletApiService palletApiService) : ControllerBase
{
    #region Queries

    [HttpGet]
    public Task<PalletDto[]> GetByDate(
        [FromQuery, DefaultValue(typeof(DateTime), "0001-01-01T00:00:00")] DateTime startDt,
        [FromQuery, DefaultValue(typeof(DateTime), "9999-12-31T23:59:59")] DateTime endDt
    ) => palletApiService.GetByDateAsync(startDt, endDt);

    [HttpGet("{number}")]
    public Task<PalletDto[]> GetByNumber([FromRoute] string number) =>
        palletApiService.GetByNumberAsync(number);

    [HttpGet("{palletId:guid}/labels")]
    public Task<LabelDto[]> GetLabelsByPallet(Guid palletId) =>
        palletApiService.GetAllZplByPalletAsync(palletId);

    #endregion

    #region Commands

    [HttpPost]
    public async Task<PalletDto> Create([FromBody] PalletPieceCreateDto dto) =>
        await palletApiService.CreatePiecePalletAsync(dto);

    [HttpDelete("{palletId:guid}")]
    public async Task Delete(Guid palletId) =>
        await palletApiService.DeleteAsync(palletId);

    #endregion
}