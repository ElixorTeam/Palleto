using Pl.Admin.Models.Features.Admins.PalletMen.Commands;
using Pl.Admin.Models.Features.Admins.PalletMen.Queries;

namespace Pl.Admin.Api.App.Features.Admins.PalletMen.Common;

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