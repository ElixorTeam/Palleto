using Ocelot.DependencyInjection;
using Ocelot.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOcelot(builder.Configuration.GetSection("Ocelot"));

WebApplication app = builder.Build();

app.MapControllers();

await app.UseOcelot();

app.Run();