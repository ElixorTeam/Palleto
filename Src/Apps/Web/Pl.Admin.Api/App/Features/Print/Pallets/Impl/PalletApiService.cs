using Pl.Admin.Api.App.Features.Print.Pallets.Common;
using Pl.Admin.Api.App.Shared.Enums;
using Pl.Admin.Api.App.Shared.Exceptions;
using Pl.Admin.Api.App.Features.Print.Pallets.Expressions;
using Pl.Admin.Models.Features.Print.Pallets;
using Pl.Shared.ValueTypes;

namespace Pl.Admin.Api.App.Features.Print.Pallets.Impl;

internal sealed class PalletApiService(
    WsDbContext dbContext
) : IPalletService
{
    #region Queries

    public async Task<PalletDto> GetByNumber(string number) =>
        await dbContext.Pallets
            .AsNoTracking()
            .Where(i => i.Number == number)
            .ToPalletDto(dbContext.Labels)
            .SingleOrDefaultAsync() ??
        throw new ApiInternalLocalizingException
        {
            PropertyName = FkProperty.Pallet.GetDescription(),
            ErrorType = ApiErrorType.NotFound
        };

    public async Task<PalletDto[]> GetPalletsWorkShiftByArmAsync(Guid armId)
    {
        WorkShift workShift = new();

        return await dbContext.Pallets
            .AsNoTracking()
            .Where(i =>
                i.CreateDt > workShift.Start.ToUniversalTime() &&
                i.CreateDt < workShift.End.ToUniversalTime() &&
                i.Arm.Id == armId
            )
            .OrderByDescending(i => i.CreateDt)
            .ToPalletDto(dbContext.Labels)
            .ToArrayAsync();
    }

    public async Task<PalletDto> GetByIdAsync(Guid id) =>
        await dbContext.Pallets
            .AsNoTracking()
            .ToPalletDto(dbContext.Labels)
            .SingleOrDefaultAsync(i => i.Id == id) ??
        throw new ApiInternalLocalizingException
        {
            PropertyName = FkProperty.Pallet.GetDescription(),
            ErrorType = ApiErrorType.NotFound
        };

    public async Task<LabelPalletDto[]> GetPalletLabels(Guid id) =>
        await dbContext.Pallets
        .AsNoTracking()
        .Where(p => p.Id == id)
        .ToLabelPalletDto(dbContext.Labels)
        .ToArrayAsync();

    #endregion
}