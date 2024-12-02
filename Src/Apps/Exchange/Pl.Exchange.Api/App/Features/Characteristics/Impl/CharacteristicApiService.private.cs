using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Storage;
using Pl.Database.Entities.Ref1C.Characteristics;
using Pl.Exchange.Api.App.Features.Characteristics.Impl.Models;

namespace Pl.Exchange.Api.App.Features.Characteristics.Impl;

internal partial class CharacteristicApiService
{
    #region Resolve uniques Db

    private void ResolveUniqueDb(HashSet<GroupedCharacteristic> dtos)
    {
        HashSet<Guid> boxUids = dtos.Select(x => x.BoxUid).ToHashSet();
        HashSet<Guid> pluUids = dtos.Select(x => x.PluUid).ToHashSet();
        HashSet<Guid> characteristicUids = dtos.Select(x => x.Uid).ToHashSet();
        HashSet<short> bundleCounts = dtos.Select(x => x.BundleCount).ToHashSet();

        List<GroupedCharacteristic> existingPairs = DbContext.Characteristics
            .Where(i =>
                !characteristicUids.Contains(i.Id) &&
                boxUids.Contains(i.BoxId) &&
                bundleCounts.Contains(i.BundleCount) &&
                pluUids.Contains(i.PluId)
            ).Select(i =>
                new GroupedCharacteristic
                {
                    Uid = i.Id,
                    PluUid = i.PluId,
                    BoxUid = i.BoxId,
                    BundleCount = i.BundleCount,
                    IsDelete = false,
                    Name = ""
                })
            .ToList();

        dtos.RemoveWhere(dto =>
        {
            if (!existingPairs.Any(uniq =>
                    dto.PluUid == uniq.PluUid &&
                    dto.BoxUid == uniq.BoxUid &&
                    dto.BundleCount == uniq.BundleCount &&
                    dto.Uid != uniq.Uid
                )) return false;
            OutputDto.AddError(dto.Uid, "Характеристика - (Box, Plu, BundleCount) не уникальна (бд)");
            return true;
        });
    }

    private void ResolveIsWeightDb(HashSet<GroupedCharacteristic> dtos)
    {
        HashSet<Guid> charUids = dtos.Select(x => x.Uid).ToHashSet();

        List<Guid> weightPlu = DbContext.Characteristics
            .Join(
                DbContext.Plus, characteristic => characteristic.PluId, plu => plu.Id,
                (characteristic, plu) => new { Characteristic = characteristic, Plu = plu }
            )
            .Where(pair => charUids.Contains(pair.Characteristic.Id) && pair.Plu.IsWeight)
            .Select(pair => pair.Plu.Id)
            .ToList();

        dtos.RemoveWhere(dto =>
        {
            if (!weightPlu.Contains(dto.PluUid)) return false;
            OutputDto.AddError(dto.Uid, "Характеристика - принадлежит весовой плу");
            return true;
        });
    }

    #endregion

    private void SaveCharacteristics(HashSet<GroupedCharacteristic> dtos)
    {
        List<CharacteristicEntity> characteristics = dtos.Select(i => i.ToEntity(DateTime.Now)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkInsertOrUpdate(characteristics, options =>
            {
                options.UseTempDB = true;
                options.UpdateByProperties = [nameof(CharacteristicEntity.Id)];
                options.PropertiesToExcludeOnUpdate = [nameof(CharacteristicEntity.CreateDt)];
            });
            transaction.Commit();
            OutputDto.AddSuccess(characteristics.ConvertAll(i => i.Id));
        }
        catch (Exception e)
        {
            logger.LogCritical($"{e.StackTrace}: {e.Message}");
            transaction.Rollback();
            OutputDto.AddError(characteristics.ConvertAll(i => i.Id), "Не предвиденная ошибка");
        }
    }

    private void DeleteCharacteristics(HashSet<GroupedCharacteristic> dtos)
    {
        List<CharacteristicEntity> characteristicToDelete =
            dtos.Where(dto => dto.IsDelete)
                .Select(dto => dto.ToEntity(DateTime.MinValue))
                .ToList();

        if (characteristicToDelete.Count == 0) return;

        HashSet<Guid> deletedUid = characteristicToDelete.ConvertAll(i => i.Id).ToHashSet();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkDelete(characteristicToDelete, config => config.UseTempDB = true);
            transaction.Commit();
            OutputDto.AddSuccess(deletedUid);
        }
        catch (Exception e)
        {
            logger.LogCritical($"{e.StackTrace}: {e.Message}");
            OutputDto.AddError(deletedUid, "Не предвиденная ошибка");
            transaction.Rollback();
        }
        finally
        {
            dtos.RemoveWhere(dto => deletedUid.Contains(dto.Uid));
        }
    }
}