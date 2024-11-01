using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace Ws.Desktop.Api.App.Features.Plu.Common;

public interface IPluWeightService
{
    #region Queries

    public Task<PluWeightDto[]> GetAllWeightAsync();

    #endregion

    #region Commands

    public Task<PrintSuccessDto> GenerateLabel(Guid pluId, CreateWeightLabelDto dto);

    #endregion
}