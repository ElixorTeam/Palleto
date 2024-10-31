using Microsoft.Data.SqlClient;
using Ws.DeviceControl.Api.App.Features.Exceptions;
using Ws.DeviceControl.Api.App.Shared.Enums;

namespace Ws.DeviceControl.Api.App.Shared.Extensions;

internal static class DbContextExtensions
{
    public static async Task SafeExistAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate, FkProperty property) where T : class
    {
        bool isExist = await dbSet.AnyAsync(predicate);
        if (!isExist)
            throw new ApiInternalLocalizingException
        {
            PropertyName = property.GetDescription(),
            ErrorType = ApiErrorType.NotFound
        };
    }

    public static async Task SafeDeleteAsync<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate, FkProperty property) where T : class
    {
        try
        {
            await dbSet.Where(predicate).ExecuteDeleteAsync();
        }
        catch (SqlException)
        {
            throw new ApiInternalLocalizingException
            {
                PropertyName = property.GetDescription(),
                ErrorType = ApiErrorType.IsUse
            };
        }
    }

    public static async Task<T> SafeGetSingleByPredicate<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> predicate, FkProperty property) where T : class
    {
        return await dbSet.SingleOrDefaultAsync(predicate) ??
               throw new ApiInternalLocalizingException
        {
            PropertyName = property.GetDescription(),
            ErrorType = ApiErrorType.NotFound
        };
    }

    public static async Task<T> SafeGetById<T>(this DbSet<T> dbSet, Guid id, FkProperty property) where T : class
    {
        T? entity = await dbSet.FindAsync(id);
        if (entity == null)
            throw new ApiInternalLocalizingException
            {
                PropertyName = property.GetDescription(),
                ErrorType = ApiErrorType.NotFound
            };
        return entity;
    }
}