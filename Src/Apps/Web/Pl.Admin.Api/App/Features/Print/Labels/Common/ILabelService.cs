using Pl.Admin.Models.Features.Print.Labels;

namespace Pl.Admin.Api.App.Features.Print.Labels.Common;

public interface ILabelService
{
    public Task<LabelDto> GetByIdAsync(Guid id);
    public Task<ZplDto> GetZplByIdAsync(Guid id);
    public Task<LabelDto> GetLabelByBarcodeAsync(string barcode);
    public Task<LabelDto[]> GetLabelsWorkShiftByArmAsync(Guid amrId);
}