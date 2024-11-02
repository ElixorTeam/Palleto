using Pl.Shared.Web.ValueTypes;

namespace Pl.Mobile.Api.App.Shared.Middlewares;

public class GenerateLabelExceptionHandlingMiddleware(ILogger<GenerateLabelExceptionHandlingMiddleware> logger) : IMiddleware
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

            ApiFailedResponse problem = new()
            {
                LocalizeMessage = e.ErrorDisplayMessage,
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}