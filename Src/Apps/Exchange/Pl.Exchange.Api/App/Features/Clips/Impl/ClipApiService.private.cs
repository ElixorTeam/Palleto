using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore.Storage;
using Pl.Database.Entities.Ref1C.Clips;
using Pl.Exchange.Api.App.Features.Clips.Dto;

namespace Pl.Exchange.Api.App.Features.Clips.Impl;

internal partial class ClipApiService
{
    private void SaveClips(HashSet<ClipDto> validDtos)
    {
        List<ClipEntity> clips = validDtos.Select(dto => dto.ToEntity(DateTime.Now)).ToList();

        using IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
        try
        {
            DbContext.BulkInsertOrUpdate(clips, options =>
            {
                options.UseTempDB = true;
                options.UpdateByProperties = [nameof(ClipEntity.Id)];
                options.PropertiesToExcludeOnUpdate = [nameof(ClipEntity.CreateDt)];
            });

            transaction.Commit();
            OutputDto.AddSuccess(clips.ConvertAll(i => i.Id));
        }
        catch (Exception e)
        {
            logger.LogCritical($"{e.StackTrace}: {e.Message}");
            OutputDto.AddError(clips.ConvertAll(i => i.Id), "Не предвиденная ошибка");
        }
    }
}