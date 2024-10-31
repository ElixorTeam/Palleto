using Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands;
using Ws.DeviceControl.Models.Features.Admins.PalletMen.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.PalletMen.Common;

public interface IPalletManService :
    IDeleteById,
    IGetById<PalletManDto>,
    IGetByProdSite<PalletManDto>
{
    #region Commands

    Task<PalletManDto> CreateAsync(PalletManCreateDto dto);
    Task<PalletManDto> UpdateAsync(Guid id, PalletManUpdateDto dto);

    #endregion
}