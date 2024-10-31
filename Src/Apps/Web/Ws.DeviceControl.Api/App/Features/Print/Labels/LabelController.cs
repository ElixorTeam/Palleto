using Ws.DeviceControl.Api.App.Features.Print.Labels.Common;
using Ws.DeviceControl.Models.Features.Print.Labels;

namespace Ws.DeviceControl.Api.App.Features.Print.Labels;

[ApiController]
[Route(ApiEndpoints.Labels)]
public sealed class LabelController(ILabelService labelService)
{
    #region Queries

    [HttpGet]
    public Task<List<LabelDto>> GetAll() => labelService.GetAllAsync();

    [HttpGet]
    public Task<List<LabelDto>> GetLabelsWorkShiftByArm([FromQuery(Name = "armId")] Guid armId) =>
        labelService.GetLabelsWorkShiftByArmAsync(armId);

    [HttpGet]
    public Task<LabelDto> GetLabelsWorkShiftByArm([FromQuery(Name = "barcode")] string barcode) =>
        labelService.GetLabelByBarcodeAsync(barcode);

    [HttpGet("{id:guid}")]
    public Task<LabelDto> GetById([FromRoute] Guid id) => labelService.GetByIdAsync(id);

    [HttpGet("{id:guid}/zpl")]
    public Task<ZplDto> GetZplById([FromRoute] Guid id) => labelService.GetZplByIdAsync(id);

    #endregion
}