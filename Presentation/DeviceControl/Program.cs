using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Tailwind;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Ws.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Add

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(options => { options.FallbackPolicy = options.DefaultPolicy; });

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddOptions();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddVsServices();
builder.Services
    .AddBlazorise()
    .AddTailwindProviders()
    .AddFontAwesomeIcons();
builder.Services.AddBlazoredLocalStorage();

builder.Host.ConfigureLogging(logging =>
{
    logging.SetMinimumLevel(LogLevel.Warning);
    logging.AddConsole();
});

#endregion

#region AddScoped

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RouteService>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<JsService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IClaimsTransformation, CustomClaimsTransformation>();

#endregion

#region App

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

SqlCoreHelper.Instance.SetSessionFactory(false);

app.Run();

#endregion
