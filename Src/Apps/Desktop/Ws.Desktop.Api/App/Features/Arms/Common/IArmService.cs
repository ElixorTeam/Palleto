using Ws.Desktop.Models.Features.Arms.Input;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Common;

public interface IArmService
{
    #region Queries

    public Task<ArmValue> GetCurrentAsync();

    #endregion

    #region Commands

    public Task UpdateAsync(UpdateArmDto dto);

    #endregion
}