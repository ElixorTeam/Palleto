using Pl.Desktop.Models.Features.Labels.Input;
using Pl.Desktop.Models.Features.Labels.Output;
using Pl.Desktop.Models.Features.Plus.Weight.Output;

namespace Pl.Desktop.Models.Api;

public interface IDesktopPluWeightApi
{
    #region Queries

    [Get("/plu/weight")]
    Task<PluWeightDto[]> GetPlusWeightByArm();

    #endregion

    #region Commands

    [Post("/plu/weight/{pluUid}/label")]
    Task<PrintSuccessDto> CreatePluWeightLabel(Guid pluUid, [Body] CreateWeightLabelDto createDto);

    #endregion
}