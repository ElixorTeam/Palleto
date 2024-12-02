using Pl.Admin.Models.Features.References1C.Plus.Commands.Update;
using Pl.Admin.Models.Features.References1C.Plus.Queries;

namespace Pl.Admin.Api.App.Features.References1C.Plus.Common;

public interface IPluService : IGetAll<PluDto>, IGetById<PluDto>
{
    #region Queries

    Task<CharacteristicDto[]> GetCharacteristics(Guid id);

    #endregion

    #region Commands

    Task<PluDto> Update(Guid id, PluUpdateDto dto);

    #endregion
}