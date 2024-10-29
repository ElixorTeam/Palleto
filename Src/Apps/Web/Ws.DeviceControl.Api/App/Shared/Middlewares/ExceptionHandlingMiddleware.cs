using Ws.DeviceControl.Api.App.Features.Exceptions;
using Ws.Shared.Web.ValueTypes;

namespace Ws.DeviceControl.Api.App.Shared.Middlewares;

public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, ErrorHelper errorHelper) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ApiInternalException e)
        {
            logger.LogWarning(e, "{EMessage}\n{EInternal}",
            e.ErrorDisplayMessage, e.ErrorInternalMessage);

            context.Response.StatusCode = (int)e.StatusCode;

            ApiFailedResponse problem = new()
            {
                LocalizeMessage = e.ErrorDisplayMessage,
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
        catch (ApiInternalLocalizingException e)
        {
            context.Response.StatusCode = e.ErrorType switch
            {
                ApiErrorType.NotFound => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.Conflict
            };

            string message = errorHelper.Localize(e);

            logger.LogWarning(e, "{EMessage}", message);

            ApiFailedResponse problem = new()
            {
                LocalizeMessage = message,
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}