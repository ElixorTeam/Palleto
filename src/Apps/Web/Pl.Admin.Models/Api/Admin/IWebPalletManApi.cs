using Pl.Admin.Models.Features.Admins.PalletMen.Commands;
using Pl.Admin.Models.Features.Admins.PalletMen.Queries;

namespace Pl.Admin.Models.Api.Admin;

public interface IWebPalletManApi
{
    #region Queries

    [Get("/pallet-men/{uid}")]
    Task<PalletManDto> GetPalletManByUid(Guid uid);

    [Get("/pallet-men?productionSite={productionSiteUid}")]
    Task<PalletManDto[]> GetPalletMenByProductionSite(Guid productionSiteUid);

    #endregion

    #region Commands

    [Delete("/pallet-men/{uid}")]
    Task DeletePalletMan(Guid uid);

    [Post("/pallet-men")]
    Task<PalletManDto> CreatePalletMan([Body] PalletManCreateDto createDto);

    [Put("/pallet-men/{uid}")]
    Task<PalletManDto> UpdatePalletMan(Guid uid, [Body] PalletManUpdateDto updateDto);

    #endregion
}