using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Ws.Desktop.Api.App.Features.Pallets.Common;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Api.App.Features.Pallets;

[ApiController]
[Route("api/arms/{armId:guid}/pallets")]
public class PalletController(IPalletApiService palletApiService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<PalletInfo>> GetByDate(
        [FromRoute] Guid armId,
        [FromQuery, DefaultValue(typeof(DateTime), "0001-01-01T00:00:00")] DateTime startDt,
        [FromQuery, DefaultValue(typeof(DateTime), "9999-12-31T23:59:59")] DateTime endDt
    ) => palletApiService.GetAllByDate(armId, startDt, endDt);

    [HttpGet("{number}")]
    public ActionResult<PalletInfo> Get([FromRoute] Guid armId, uint number) =>
        palletApiService.GetByNumber(armId, number) is { } arm ? Ok(arm) : NotFound();

    [HttpGet("{palletId:guid}/labels")]
    public ActionResult<List<PalletInfo>> GetLabelsByPallet([FromRoute] Guid armId, Guid palletId) =>
        Ok(palletApiService.GetAllZplByArm(armId, palletId));

    [HttpPost]
    public ActionResult<PalletInfo> Create([FromRoute] Guid armId, [FromBody] PalletPieceCreateDto dto) =>
        Ok(palletApiService.CreatePiecePallet(armId, dto));
}