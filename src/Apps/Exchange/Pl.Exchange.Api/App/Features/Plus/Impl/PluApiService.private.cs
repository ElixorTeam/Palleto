using EFCore.BulkExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Pl.Database.Entities.Ref1C.Nestings;
using Pl.Database.Entities.Ref1C.Plus;
using Pl.Exchange.Api.App.Features.Plus.Dto;

namespace Pl.Exchange.Api.App.Features.Plus.Impl;

internal sealed partial class PluApiService
{
    #region Resolve uniques db

    private void ResolveUniqueNumberDb(HashSet<PluDto> dtos)
    {
        HashSet<short> numbers = dtos.Select(dto => dto.Number).ToHashSet();
        HashSet<Guid> plusUid = dtos.Select(dto => dto.Uid).ToHashSet();

        List<short> existingNumbers = DbContext.Plus
            .Where(i => !plusUid.Contains(i.Id) && numbers.Contains(i.Number))
            .Select(i => i.Number)
            .ToList();

        dtos.RemoveWhere(dto =>
        {
            if (!existingNumbers.Contains(dto.Number)) return false;
            OutputDto.AddError(dto.Uid, $"Номер плу - ({dto.Number}) не уникален (бд)");
            return true;
        });
    }

    #endregion

    private static void SetDefaultFk(HashSet<PluDto> validDtos)
    {
        foreach (PluDto i in validDtos)
        {
            i.ClipUid = i.ClipUid.IsEmpty() ? DefaultTypes.GuidMax : i.ClipUid;
            i.BundleUid = i.BundleUid.IsEmpty() ? DefaultTypes.GuidMax : i.BundleUid;
        }
    }

    private void SavePlus(HashSet<PluDto> validDtos)
    {
        List<PluEntity> plus = validDtos.Select(dto => dto.ToPluEntity(DateTime.Now)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkInsertOrUpdate(plus, options =>
            {
                options.UseTempDB = true;
                options.UpdateByProperties = [nameof(PluEntity.Id)];
                options.PropertiesToExcludeOnUpdate = [nameof(PluEntity.CreateDt), nameof(PluEntity.TemplateId)];
            });

            transaction.Commit();
            SaveNestings(validDtos);
            OutputDto.AddSuccess(plus.ConvertAll(i => i.Id));
        }
        catch (Exception e)
        {
            logger.LogCritical($"{e.StackTrace}: {e.Message}");
            transaction.Rollback();
            OutputDto.AddError(plus.ConvertAll(i => i.Id), "Не предвиденная ошибка");
        }
    }

    private void DeleteNestings(HashSet<PluDto> dtos)
    {
        List<Guid> uidToDelete = dtos.Where(dto => dto.IsDelete).Select(dto => dto.Uid).ToList();

        if (uidToDelete.Count == 0) return;

        IDbContextTransaction transaction = DbContext.Database.BeginTransaction();

        try
        {
            List<object> parameters = uidToDelete.Select((uid, index) => new SqlParameter($"@p{index}", uid)).ToList<object>();

            string inClause = string.Join(", ", parameters.Select((_, index) => $"@p{index}"));
            string sql = $@"
                DELETE FROM [dbo].[ARMS_PLUS_FK] WHERE PLU_UID IN ({inClause});
                UPDATE [PRINT].[LABELS] SET PLU_UID = NULL WHERE PLU_UID IN ({inClause});
                DELETE FROM [REF_1C].[PLUS] WHERE UID IN ({inClause});
            ";
            DbContext.Database.ExecuteSqlRaw(sql, parameters.ToArray());

            transaction.Commit();
            OutputDto.AddSuccess(uidToDelete);
        }
        catch (Exception e)
        {
            logger.LogCritical($"{e.StackTrace}: {e.Message}");
            OutputDto.AddError(uidToDelete, "Не предвиденная ошибка");
            transaction.Rollback();
        }
        finally
        {
            dtos.RemoveWhere(dto => uidToDelete.Contains(dto.Uid));
        }
    }

    private void SaveNestings(HashSet<PluDto> validDtos)
    {
        List<NestingEntity> nestings = validDtos.Select(dto => dto.ToNestingEntity(DateTime.Now)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkInsertOrUpdate(nestings, options =>
            {
                options.UseTempDB = true;
                options.UpdateByProperties = [nameof(NestingEntity.Id)];
                options.PropertiesToExcludeOnUpdate = [nameof(NestingEntity.CreateDt)];
            });
            transaction.Commit();
        }
        catch (Exception e)
        {
            logger.LogCritical($"{e.StackTrace}: {e.Message}");
            transaction.Rollback();
        }
    }
}