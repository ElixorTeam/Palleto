using Ws.DeviceControl.Api.App.Features.Print.Pallets.Common;
using Ws.DeviceControl.Api.App.Features.Print.Pallets.Expressions;
using Ws.DeviceControl.Api.App.Shared.Enums;
using Ws.DeviceControl.Api.App.Shared.Exceptions;
using Ws.DeviceControl.Models.Features.Print.Pallets;
using Ws.Shared.ValueTypes;

namespace Ws.DeviceControl.Api.App.Features.Print.Pallets.Impl;

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

    public async Task<List<PalletDto>> GetPalletsWorkShiftByArmAsync(Guid armId)
    {
        WorkShift workShift = new();

        return await dbContext.Pallets
            .AsNoTracking()
            .Where(i => i.CreateDt > workShift.Start && i.CreateDt < workShift.End && i.Arm.Id == armId)
            .OrderByDescending(i => i.CreateDt)
            .ToPalletDto(dbContext.Labels)
            .ToListAsync();
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

    public async Task<List<LabelPalletDto>> GetPalletLabels(Guid id) =>
        await dbContext.Pallets
        .AsNoTracking()
        .Where(p => p.Id == id)
        .ToLabelPalletDto(dbContext.Labels)
        .ToListAsync();

    #endregion
}