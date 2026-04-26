using Pl.Admin.Models.Features.Print.Labels;

namespace Pl.Admin.Models.Api.Print;

public interface IWebLabelApi
{
    #region Queries

    [Get("/labels/arm/{armId}")]
    Task<LabelDto[]> GetLabelsWorkShiftByArm(Guid armId);

    [Get("/labels/barcode/{barcode}")]
    Task<LabelDto> GetLabelByBarcode(string barcode);

    [Obsolete("Use GetLabelsWorkShiftByArm instead")]
    [Get("/labels")]
    Task<LabelDto[]> GetLabels();

    [Get("/labels/{uid}")]
    Task<LabelDto> GetLabelByUid(Guid uid);

    [Get("/labels/{uid}/zpl")]
    Task<ZplDto> GetLabelZplByUid(Guid uid);

    #endregion
}