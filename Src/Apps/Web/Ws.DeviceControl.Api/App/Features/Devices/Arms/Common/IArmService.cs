using Ws.DeviceControl.Models.Features.Devices.Arms.Commands;
using Ws.DeviceControl.Models.Features.Devices.Arms.Queries;

namespace Ws.DeviceControl.Api.App.Features.Devices.Arms.Common;

public interface IArmService :
    IDeleteById,
    IGetById<ArmDto>,
    IGetByProdSite<ArmDto>
{
    #region Queries

    Task<PluArmDto[]> GetArmPlus(Guid id);

    #endregion

    #region Commands

    Task<ArmDto> CreateAsync(ArmCreateDto dto);
    Task<ArmDto> UpdateAsync(Guid id, ArmUpdateDto dto);
    Task AddPluAsync(Guid id, Guid pluId);
    Task DeletePluAsync(Guid armId, Guid pluId);

    #endregion
}