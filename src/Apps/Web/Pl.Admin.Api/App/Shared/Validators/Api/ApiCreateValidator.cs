using LinqKit;
using Pl.Admin.Api.App.Shared.Exceptions;
using Pl.Admin.Api.App.Shared.Validators.Api.Models;

namespace Pl.Admin.Api.App.Shared.Validators.Api;

public abstract class ApiCreateValidator<TEntity, TDto> : BaseApiValidator<TDto>
    where TEntity : class
    where TDto : class
{
    public abstract Task ValidateAsync(DbSet<TEntity> dbSet, TDto dto);

    protected async Task ValidatePredicatesAsync(DbSet<TEntity> dbSet, List<PredicateField<TEntity>> predicates)
    {
        foreach (PredicateField<TEntity> predicate in predicates)
        {
            bool isExist = await dbSet.AsExpandable().AnyAsync(predicate.Predicate);

            if (isExist)
                throw new ApiInternalLocalizingException
                {
                    PropertyName =  predicate.FieldName,
                    ErrorType = ApiErrorType.Unique
                };
        }
    }
}