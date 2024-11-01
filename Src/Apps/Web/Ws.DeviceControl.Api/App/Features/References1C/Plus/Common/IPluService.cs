using Ws.DeviceControl.Models.Features.References1C.Plus.Commands.Update;
using Ws.DeviceControl.Models.Features.References1C.Plus.Queries;

namespace Ws.DeviceControl.Api.App.Features.References1C.Plus.Common;

public interface IPluService : IGetAll<PluDto>, IGetById<PluDto>
{
    #region Queries

    Task<CharacteristicDto[]> GetCharacteristics(Guid id);

    #endregion

    #region Commands

    Task<PluDto> Update(Guid id, PluUpdateDto dto);

    #endregion
}