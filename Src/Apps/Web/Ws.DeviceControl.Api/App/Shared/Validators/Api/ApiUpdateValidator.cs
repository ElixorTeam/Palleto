using LinqKit;
using Ws.DeviceControl.Api.App.Shared.Exceptions;
using Ws.DeviceControl.Api.App.Shared.Validators.Api.Models;

namespace Ws.DeviceControl.Api.App.Shared.Validators.Api;

public abstract class ApiUpdateValidator<TEntity, TDto, TId> : BaseApiValidator<TDto>
    where TEntity : class
    where TDto : class
{
    public abstract Task<TEntity> ValidateAndGetAsync(DbSet<TEntity> dbSet, TDto dto, TId id);

    protected async Task<TEntity> ValidatePredicatesAsync(DbSet<TEntity> dbSet, List<PredicateField<TEntity>> predicates,
        Expression<Func<TEntity, bool>> idExpression)
    {
        foreach (PredicateField<TEntity> predicate in predicates)
        {
            bool isExist = await dbSet.AsExpandable().AnyAsync(predicate.Predicate.And(idExpression.Not()));

            if (isExist)
                throw new ApiInternalLocalizingException
                {
                    PropertyName = predicate.FieldName,
                    ErrorType = ApiErrorType.Unique
                };
        }

        return await dbSet.FirstOrDefaultAsync(idExpression) ?? throw new ApiInternalLocalizingException
        {
            PropertyName = "Entity",
            ErrorType = ApiErrorType.NotFound
        };
    }
}