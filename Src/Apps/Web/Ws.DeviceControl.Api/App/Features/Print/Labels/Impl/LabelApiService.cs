using Ws.Database.Entities.Print.Labels;
using Ws.DeviceControl.Api.App.Features.Print.Labels.Common;
using Ws.DeviceControl.Api.App.Features.Print.Labels.Impl.Expressions;
using Ws.DeviceControl.Api.App.Shared.Enums;
using Ws.DeviceControl.Models.Features.Print.Labels;
using Ws.Shared.ValueTypes;

namespace Ws.DeviceControl.Api.App.Features.Print.Labels.Impl;

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

    public Task<List<LabelDto>> GetAllAsync() => dbContext.Labels
        .AsNoTracking()
        .OrderByDescending(i => i.CreateDt)
        .Select(LabelExpressions.ToLabelDto)
        .ToListAsync();

    public async Task<ZplDto> GetZplByIdAsync(Guid id) =>
        LabelExpressions.ToZplDto.Compile().Invoke(await dbContext.LabelZpl.SafeGetById(id, FkProperty.Label));

    public async Task<LabelDto> GetLabelByBarcodeAsync(string barcode)
    {
        LabelEntity entity =
            await dbContext.Labels.SafeGetSingleByPredicate(i => i.BarcodeTop == barcode, FkProperty.Label);
        await LoadDefaultForeignKeysAsync(entity);
        return LabelExpressions.ToLabelDto.Compile().Invoke(entity);
    }

    public async Task<List<LabelDto>> GetLabelsWorkShiftByArmAsync(Guid amrId)
    {
        WorkShift workShift = new();

        return await dbContext.Labels
            .AsNoTracking()
            .Where(i => i.CreateDt > workShift.Start && i.CreateDt < workShift.End && i.LineId == amrId && i.IsWeight)
            .OrderByDescending(i => i.CreateDt)
            .Select(LabelExpressions.ToLabelDto)
            .ToListAsync();
    }

    #endregion

    #region Private

    private async Task LoadDefaultForeignKeysAsync(LabelEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.Plu).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Line).LoadAsync();
        await dbContext.Entry(entity).Reference(e => e.Pallet).LoadAsync();
        await dbContext.Entry(entity.Line).Reference(e => e.Warehouse).LoadAsync();
        await dbContext.Entry(entity.Line.Warehouse).Reference(e => e.ProductionSite).LoadAsync();
    }

    #endregion
}