using Pl.Admin.Api.App.Shared.Exceptions;
using Pl.Shared.Web.ValueTypes;

namespace Pl.Admin.Api.App.Shared.Middlewares;

public class ApiLocalizingExceptionMiddleware(ILogger<ApiLocalizingExceptionMiddleware> logger, ErrorHelper errorHelper) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
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

            context.Response.ContentType = "application/json";

            ApiFailedResponse problem = new()
            {
                LocalizeMessage = message,
            };

            string json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }
    }
}