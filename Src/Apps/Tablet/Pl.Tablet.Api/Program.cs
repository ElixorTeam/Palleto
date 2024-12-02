using Microsoft.AspNetCore.Mvc.Authorization;
using Pl.Shared.Web.Extensions;
using Pl.Tablet.Api;
using Pl.Tablet.Api.App.Shared.Auth;
using Pl.Tablet.Api.App.Shared.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHelpers<ITabletApiAssembly>()
    .AddMiddlewares<ITabletApiAssembly>()
    .AddApiServices<ITabletApiAssembly>();

builder.Services
    .AddLocalization()
    .AddHttpContextAccessor()
    .AddEndpointsApiExplorer()
    .AddControllers(options =>
    {
        options.Filters.Add(new AuthorizeFilter());
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

builder.Services
    .AddAuthentication(ArmAuthenticationOptions.DefaultScheme)
    .AddScheme<ArmAuthenticationOptions, ArmAuthenticationHandler>(
        ArmAuthenticationOptions.DefaultScheme, _ => { }
    );

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseApiLocalization();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GenerateLabelExceptionHandlingMiddleware>();

app.Run();