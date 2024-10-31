namespace Ws.DeviceControl.Api.App.Common;

public interface IGetAll<T>
{
    public Task<T[]> GetAllAsync();
}