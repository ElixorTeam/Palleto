using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Storage;
using Pl.Database.Entities.Ref1C.Brands;
using Pl.Exchange.Api.App.Features.Brands.Dto;

namespace Pl.Exchange.Api.App.Features.Brands.Impl;

internal partial class BrandApiService
{
    #region Resolve uniques db

    private void ResolveUniqueNameDb(HashSet<BrandDto> dtos)
    {
        HashSet<string> namesList = dtos.Select(dto => dto.Name).ToHashSet();
        HashSet<Guid> uidList = dtos.Select(dto => dto.Uid).ToHashSet();

        HashSet<string> existingNames = DbContext.Brands
            .Where(brand =>
                namesList.Any(name => name.Equals(brand.Name)) &&
                !uidList.Any(uid => uid.Equals(brand.Id))
            )
            .Select(brand => brand.Name)
            .ToHashSet();

        dtos.RemoveWhere(dto =>
        {
            if (!existingNames.Contains(dto.Name)) return false;
            OutputDto.AddError(dto.Uid, "Наименование не уникально (бд)");
            return true;
        });
    }

    #endregion

    private void SaveBrands(HashSet<BrandDto> validDtos)
    {
        List<BrandEntity> brands = validDtos.Select(dto => dto.ToEntity(DateTime.Now)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkInsertOrUpdate(brands, options =>
            {
                options.UseTempDB = true;
                options.UpdateByProperties = [nameof(BrandEntity.Id)];
                options.PropertiesToExcludeOnUpdate = [nameof(BrandEntity.CreateDt)];
            });

            transaction.Commit();

            OutputDto.AddSuccess(brands.ConvertAll(i => i.Id));
        }
        catch (Exception e)
        {
            logger.LogCritical($"{e.StackTrace}: {e.Message}");
            transaction.Rollback();
            OutputDto.AddError(brands.ConvertAll(i => i.Id), "Не предвиденная ошибка");
        }
    }
}