namespace Pl.Admin.Api.App.Common;

public interface IGetAll<T>
{
    public Task<T[]> GetAllAsync();
}