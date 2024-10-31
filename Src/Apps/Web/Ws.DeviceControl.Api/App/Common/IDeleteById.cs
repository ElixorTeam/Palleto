namespace Ws.DeviceControl.Api.App.Common;

public interface IDeleteById
{
    Task DeleteAsync(Guid id);
}