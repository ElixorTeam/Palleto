using Microsoft.AspNetCore.Mvc.Authorization;
using Pl.Database;
using Pl.Desktop.Api;
using Pl.Desktop.Api.App.Shared.Labels;
using Pl.Desktop.Api.App.Shared.Labels.Settings;
using Pl.Desktop.Api.App.Shared.Middlewares;
using Pl.Shared.Web.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

PalychSettings palychSettings = builder.Configuration
    .GetSection("Palych").Get<PalychSettings>() ?? throw new NullReferenceException();

builder.Services
    .AddLocalization()
    .AddAuthorization(PolicyAuthUtils.RegisterAuthorization)
    .AddAuthentication(ArmAuthenticationOptions.DefaultScheme)
    .AddScheme<ArmAuthenticationOptions, ArmAuthenticationHandler>(
         ArmAuthenticationOptions.DefaultScheme, _ => { }
    );

builder.Services
    .AddEfCore()
    .AddLabelsServices(palychSettings)
    .AddHelpers<IDesktopApiAssembly>()
    .AddMiddlewares<IDesktopApiAssembly>()
    .AddApiServices<IDesktopApiAssembly>();

builder.Services
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

builder.Services.AddHttpContextAccessor();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseApiLocalization();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GenerateLabelExceptionHandlingMiddleware>();

app.Run();