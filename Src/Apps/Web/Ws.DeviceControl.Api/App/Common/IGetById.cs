namespace Ws.DeviceControl.Api.App.Common;

public interface IGetById<T>
{
    public Task<T> GetByIdAsync(Guid id);
}