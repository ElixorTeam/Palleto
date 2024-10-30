using Ws.DeviceControl.Api;
using Ws.DeviceControl.Api.App.Shared.Middlewares;
using Ws.Shared.Web.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEfCore()
    .AddLocalization()
    .AddHttpContextAccessor()
    .AddEndpointsApiExplorer();

builder.Services
    .BaseSetup()
    .AddUserClaims()
    .AddHelpers<IDeviceControlApiAssembly>()
    .AddValidators<IDeviceControlApiAssembly>()
    .AddApiServices<IDeviceControlApiAssembly>()
    .AddMiddlewares<IDeviceControlApiAssembly>()
    .AddAuth(builder.Configuration);

WebApplication app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseApiLocalization();

app.UseHttpsRedirection();
app.MapControllers();

app.UseMiddleware<ApiInternalExceptionMiddleware>();
app.UseMiddleware<ApiLocalizingExceptionMiddleware>();

app.Run();