using Microsoft.EntityFrameworkCore;
using Ws.Database;
using Ws.Desktop.Api.App.Features.PalletMen.Common;
using Ws.Desktop.Api.App.Features.PalletMen.Expressions;
using Ws.Desktop.Models.Features.PalletMen;

namespace Ws.Desktop.Api.App.Features.PalletMen.Impl;

internal sealed class PalletManApiService(WsDbContext dbContext, UserHelper userHelper) : IPalletManService
{
    #region Queries

    public async Task<PalletMan> GetByCodeAsync(string code)
    {
        PalletMan? palletMan = await dbContext.PalletMen
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