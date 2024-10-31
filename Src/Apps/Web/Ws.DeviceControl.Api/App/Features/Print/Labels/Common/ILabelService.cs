using Ws.DeviceControl.Models.Features.Print.Labels;

namespace Ws.DeviceControl.Api.App.Features.Print.Labels.Common;

public interface ILabelService
{
    public Task<LabelDto> GetByIdAsync(Guid id);
    public Task<ZplDto> GetZplByIdAsync(Guid id);
    public Task<LabelDto> GetLabelByBarcodeAsync(string barcode);
    public Task<List<LabelDto>> GetLabelsWorkShiftByArmAsync(Guid amrId);
}