using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Pl.Database;
using Pl.Database.Common;

namespace Pl.Exchange.Api.App.Common;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal abstract class BaseService<TDto>(IValidator<TDto> validator) where TDto : BaseDto
{
    private readonly IValidator _validator = validator;
    protected readonly WsDbContext DbContext = new();
    protected readonly ResponseDto OutputDto = new();

    # region ResolveLocal

    protected void FilterValidDtos(HashSet<TDto> dtos)
    {
        dtos.RemoveWhere(IsNotValid);
        return;

        bool IsNotValid(TDto dto)
        {
            ValidationResult validationResult = _validator.Validate(new ValidationContext<TDto>(dto));
            if (validationResult.IsValid) return false;

            OutputDto.AddError(dto.Uid, validationResult.Errors[0].ErrorMessage);
            return true;
        }
    }

    protected void ResolveUniqueUidLocal(HashSet<TDto> dtos) =>
        ResolveUniqueLocal(dtos, arg => arg.Uid, "Uid - не уникален");

    protected void ResolveUniqueLocal<TKey>(HashSet<TDto> dtos, Func<TDto, TKey> groupBy, string msg)
    {
        HashSet<TKey> seenKeys = [];
        HashSet<Guid> duplicatesKeys = [];

        foreach (TDto dto in dtos)
        {
            TKey key = groupBy(dto);
            if (!seenKeys.Add(key))
                duplicatesKeys.Add(dto.Uid);
        }

        dtos.RemoveWhere(IsNotValid);
        return;

        bool IsNotValid(TDto dto)
        {
            if (!duplicatesKeys.Contains(dto.Uid))
                return false;
            OutputDto.AddError(dto.Uid, msg);
            return true;
        }
    }

    # endregion

    protected void ResolveNotExistsFkDb<TEntity>(HashSet<TDto> dtos, DbSet<TEntity> dbSet, Func<TDto, Guid> select, string msg)
        where TEntity : EfEntityBase
    {
        HashSet<Guid> uidsFkList = dtos.Select(select).ToHashSet();

        HashSet<Guid> existingInDb = dbSet
            .Where(entity => uidsFkList.Contains(entity.Id))
            .Select(entity => entity.Id)
            .ToHashSet();

        dtos.RemoveWhere(IsNotValid);
        return;

        bool IsNotValid(TDto dto)
        {
            Guid id = select(dto);
            if (existingInDb.Contains(id))
                return false;
            OutputDto.AddError(id, msg);
            return true;
        }
    }
}