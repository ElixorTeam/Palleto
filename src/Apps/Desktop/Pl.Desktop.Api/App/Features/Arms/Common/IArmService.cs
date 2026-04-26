using Pl.Desktop.Models.Features.Arms.Input;
using Pl.Desktop.Models.Features.Arms.Output;

namespace Pl.Desktop.Api.App.Features.Arms.Common;

public interface IArmService
{
    #region Queries

    public Task<ArmDto> GetCurrentAsync();

    #endregion

    #region Commands

    public Task UpdateAsync(UpdateArmDto dto);

    #endregion
}