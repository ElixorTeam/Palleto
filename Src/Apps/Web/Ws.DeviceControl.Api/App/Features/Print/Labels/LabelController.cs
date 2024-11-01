using Ws.DeviceControl.Api.App.Features.Print.Labels.Common;
using Ws.DeviceControl.Models.Features.Print.Labels;

namespace Ws.DeviceControl.Api.App.Features.Print.Labels;

[ApiController]
[Route(ApiEndpoints.Labels)]
public sealed class LabelController(ILabelService labelService)
{
    #region Queries

    [HttpGet("arm/{armId:guid}")]
    public Task<LabelDto[]> GetLabelsWorkShiftByArm(Guid armId) =>
        labelService.GetLabelsWorkShiftByArmAsync(armId);

    [HttpGet("barcode/{barcode}")]
    public Task<LabelDto> GetLabelByBarcodeAsync(string barcode) =>
        labelService.GetLabelByBarcodeAsync(barcode);

    [HttpGet("{id:guid}")]
    public Task<LabelDto> GetById([FromRoute] Guid id) =>
        labelService.GetByIdAsync(id);

    [HttpGet("{id:guid}/zpl")]
    public Task<ZplDto> GetZplById([FromRoute] Guid id) =>
        labelService.GetZplByIdAsync(id);

    #endregion
}