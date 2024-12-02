namespace Pl.Admin.Api.App.Common;

public interface IDeleteById
{
    Task DeleteAsync(Guid id);
}