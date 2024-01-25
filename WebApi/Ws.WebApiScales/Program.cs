using Microsoft.AspNetCore.Mvc.Formatters;
using Ws.Domain.Services;
using Ws.Domain.Services.Features.Clip;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Boxes.Services;
using Ws.WebApiScales.Features.Brand.Services;
using Ws.WebApiScales.Features.Brands.Services;
using Ws.WebApiScales.Features.Bundles.Services;
using Ws.WebApiScales.Features.Clips.Services;
using Ws.WebApiScales.Features.Nesting.Services;
using Ws.WebApiScales.Features.PluOld.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ResponseDto>();

#region Ready

builder.Services.AddScoped<IBoxApiService, BoxApiService>();
builder.Services.AddScoped<IClipApiService, ClipApiService>();
builder.Services.AddScoped<IBundleApiService, BundleApiService>();

#endregion


builder.Services.AddScoped<IPluApiService, PluApiService>();
builder.Services.AddScoped<IBrandApiService, BrandApiService>();
builder.Services.AddScoped<IPluCharacteristicApiService, PluCharacteristicApiService>();

builder.Services.AddDomainServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
}).AddXmlDataContractSerializerFormatters();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.MapControllers();
app.Run();
