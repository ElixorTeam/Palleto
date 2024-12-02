using Microsoft.AspNetCore.Mvc.Authorization;
using Pl.Mobile.Api;
using Pl.Mobile.Api.App.Shared.Middlewares;
using Pl.Shared.Web.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHelpers<IMobileApiAssembly>()
    .AddMiddlewares<IMobileApiAssembly>()
    .AddApiServices<IMobileApiAssembly>();

builder.Services
    .AddLocalization()
    .AddHttpContextAccessor()
    .AddEndpointsApiExplorer()
    .AddControllers(options =>
    {
        options.Filters.Add(new AllowAnonymousFilter());
        options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressConsumesConstraintForFormFileParameters = true;
        options.SuppressInferBindingSourcesForParameters = true;
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressMapClientErrors = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
    });

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseApiLocalization();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GenerateLabelExceptionHandlingMiddleware>();

app.Run();