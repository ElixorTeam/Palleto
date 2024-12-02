using Pl.Admin.Api.App.Features.Print.Labels.Common;
using Pl.Admin.Api.App.Features.Print.Labels.Impl.Expressions;
using Pl.Admin.Api.App.Shared.Enums;
using Pl.Database.Entities.Print.Labels;
using Pl.Admin.Models.Features.Print.Labels;
using Pl.Shared.ValueTypes;

namespace Pl.Admin.Api.App.Features.Print.Labels.Impl;

internal sealed class LabelApiService(
    WsDbContext dbContext
) : ILabelService
{
    #region Queries

    public async Task<LabelDto> GetByIdAsync(Guid id)
    {
        LabelEntity entity = await dbContext.Labels.SafeGetById(id, FkProperty.Label);
        await LoadDefaultForeignKeysAsync(entity);
        return LabelExpressions.ToLabelDto.Compile().Invoke(entity);
    }

    public async Task<ZplDto> GetZplByIdAsync(Guid id) =>
        LabelExpressions.ToZplDto.Compile().Invoke(await dbContext.LabelZpl.SafeGetById(id, FkProperty.Label));

    public async Task<LabelDto> GetLabelByBarcodeAsync(string barcode)
    {
        LabelEntity entity =
            await dbContext.Labels.SafeGetSingleByPredicate(i => i.BarcodeTop == barcode, FkProperty.Label);
        await LoadDefaultForeignKeysAsync(entity);
        return LabelExpressions.ToLabelDto.Compile().Invoke(entity);
    }

    public async Task<LabelDto[]> GetLabelsWorkShiftByArmAsync(Guid amrId)
    {
        WorkShift workShift = new();

        return await dbContext.Labels
            .AsNoTracking()
            .Where(i =>
                i.CreateDt > workShift.Start.ToUniversalTime() &&
                i.CreateDt < workShift.End.ToUniversalTime() &&
                i.ArmId == amrId &&
                i.PalletId == null
            )
            .OrderByDescending(i => i.CreateDt)
            .Select(LabelExpressions.ToLabelDto)
            .ToArrayAsync();
    }

    #endregion

    #region Private

    private async Task LoadDefaultForeignKeysAsync(LabelEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.Plu).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Arm).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Pallet).LoadAsync();
        await dbContext.Entry(entity.Arm).Reference(e => e.Warehouse).LoadAsync();
        await dbContext.Entry(entity.Arm.Warehouse).Reference(e => e.ProductionSite).LoadAsync();
    }

    #endregion
}