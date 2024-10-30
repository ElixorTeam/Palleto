using Ws.Shared.Web.ValueTypes;

namespace Ws.DeviceControl.Api.App.Shared.Middlewares;

public class ApiInternalExceptionMiddleware(ILogger<ApiInternalExceptionMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ApiInternalException e)
        {
            logger.LogWarning(e, "{EMessage}\n{EInternal}", e.ErrorDisplayMessage, e.ErrorInternalMessage);

            context.Response.StatusCode = (int)e.StatusCode;
            context.Response.ContentType = "application/json";

            ApiFailedResponse problem = new()
            {
                LocalizeMessage = e.ErrorDisplayMessage,
            };

            string json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }
    }
}