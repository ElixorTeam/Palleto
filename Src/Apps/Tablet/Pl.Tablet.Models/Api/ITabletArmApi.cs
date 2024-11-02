using Pl.Tablet.Models.Features.Arms;

namespace Pl.Tablet.Models.Api;

public interface ITabletArmApi
{
    [Get("/arms")]
    Task<ArmDto> GetCurrentArm();
}