using Pl.Desktop.Models.Features.Labels.Input;
using Pl.Desktop.Models.Features.Labels.Output;
using Pl.Desktop.Models.Features.Plus.Weight.Output;

namespace Pl.Desktop.Api.App.Features.Plu.Common;

public interface IPluWeightService
{
    #region Queries

    public Task<PluWeightDto[]> GetAllWeightAsync();

    #endregion

    #region Commands

    public Task<PrintSuccessDto> GenerateLabel(Guid pluId, CreateWeightLabelDto dto);

    #endregion
}