using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Storage;
using Pl.Database.Entities.Ref1C.Bundles;
using Pl.Exchange.Api.App.Features.Bundles.Dto;

namespace Pl.Exchange.Api.App.Features.Bundles.Impl;

internal partial class BundleApiService
{
    private void SaveBundles(HashSet<BundleDto> validDtos)
    {
        List<BundleEntity> bundles = validDtos.Select(dto => dto.ToEntity(DateTime.Now)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkInsertOrUpdate(bundles, options =>
            {
                options.UseTempDB = true;
                options.UpdateByProperties = [nameof(BundleEntity.Id)];
                options.PropertiesToExcludeOnUpdate = [nameof(BundleEntity.CreateDt)];
            });
            transaction.Commit();
            OutputDto.AddSuccess(bundles.ConvertAll(i => i.Id));
        }
        catch (Exception e)
        {
            logger.LogCritical($"{e.StackTrace}: {e.Message}");
            transaction.Rollback();
            OutputDto.AddError(bundles.ConvertAll(i => i.Id), "Не предвиденная ошибка");
        }
    }
}