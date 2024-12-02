using Microsoft.EntityFrameworkCore;
using Pl.Database;
using Pl.Desktop.Api.App.Features.PalletMen.Common;
using Pl.Desktop.Api.App.Features.PalletMen.Expressions;
using Pl.Desktop.Models.Features.PalletMen;

namespace Pl.Desktop.Api.App.Features.PalletMen.Impl;

internal sealed class PalletManApiService(WsDbContext dbContext, UserHelper userHelper) : IPalletManService
{
    #region Queries

    public async Task<PalletManDto> GetByCodeAsync(string code)
    {
        PalletManDto? palletMan = await dbContext.PalletMen
            .AsNoTracking()
            .Where(i => i.Warehouse.Id == userHelper.WarehouseId && i.Password == code)
            .Select(PalletManExpressions.ToDto)
            .FirstOrDefaultAsync();

        return palletMan ?? throw new ApiInternalException
        {
            ErrorDisplayMessage = "Пользователь не найден",
            StatusCode = HttpStatusCode.NotFound
        };
    }

    #endregion
}