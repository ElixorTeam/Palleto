using Microsoft.AspNetCore.Mvc.Authorization;
using Pl.Database;
using Pl.Exchange.Api;
using Pl.Exchange.Api.App.Shared.Middlewares;
using Pl.Shared.Web.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add(new AllowAnonymousFilter());
        options.Filters.Add(new ProducesAttribute("application/xml"));
    })
    .AddXmlSerializerFormatters();

builder.Services
    .AddEfCore()
    .AddValidators<IExchangeAssembly>()
    .AddMiddlewares<IExchangeAssembly>()
    .AddApiServices<IExchangeAssembly>();

#pragma warning disable CA1416

builder.Logging.AddEventLog(eventLogSetting =>
{
    eventLogSetting.Filter = (providerName, _) => !providerName.StartsWith("Microsoft");
    eventLogSetting.SourceName = "Pl.Exchange.Api";
});

#pragma warning restore CA1416

WebApplication app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseRouting();
app.MapControllers();

app.Run();