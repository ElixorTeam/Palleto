using Pl.Desktop.Models.Features.Arms.Input;
using Pl.Desktop.Models.Features.Arms.Output;

namespace Pl.Desktop.Models.Api;

public interface IDesktopArmApi
{
    #region Queries

    [Get("/arms")]
    Task<ArmDto> GetCurrentArm();

    #endregion

    #region Commands

    [Put("/arms")]
    Task UpdateArm([Body] UpdateArmDto updateDto);

    #endregion
}