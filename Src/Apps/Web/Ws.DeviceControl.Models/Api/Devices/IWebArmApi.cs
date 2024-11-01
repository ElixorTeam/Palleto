using Ws.DeviceControl.Models.Features.Devices.Arms.Commands;
using Ws.DeviceControl.Models.Features.Devices.Arms.Queries;

namespace Ws.DeviceControl.Models.Api.Devices;

public interface IWebArmApi
{
    #region Queries

    [Get("/arms/{uid}")]
    Task<ArmDto> GetArmByUid(Guid uid);

    [Get("/arms?productionSite={productionSiteUid}")]
    Task<ArmDto[]> GetArmsByProductionSite(Guid productionSiteUid);

    [Get("/arms/{uid}/plus")]
    Task<PluArmDto[]> GetArmPlus(Guid uid);

    [Get("/arms/{uid}/analytics")]
    Task<PluArmDto[]> GetArmAnalytic(Guid uid, DateOnly date);

    #endregion

    #region Commands

    [Delete("/arms/{uid}")]
    Task DeleteArm(Guid uid);

    [Post("/arms")]
    Task<ArmDto> CreateArm([Body] ArmCreateDto createDto);

    [Put("/arms/{uid}")]
    Task<ArmDto> UpdateArm(Guid uid, [Body] ArmUpdateDto updateDto);

    [Delete("/arms/{uid}/plus/{pluId}")]
    Task DeleteArmPlu(Guid uid, Guid pluId);

    [Post("/arms/{uid}/plus/{pluId}")]
    Task AddArmPlu(Guid uid, Guid pluId);

    #endregion
}