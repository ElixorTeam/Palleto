// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Files;
using DataCore.Sql.Core;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using NHibernate;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the default policy.
//    options.FallbackPolicy = options.DefaultPolicy;
//});

// NHibernate & JsonSettings & DataAccess.
JsonSettingsHelper.Instance.SetupWebApp(builder.Environment.ContentRootPath, nameof(WebApiScales));
builder.Services.AddSingleton(DataAccessHelper.Instance.SessionFactory);
builder.Services.AddScoped(factory => DataAccessHelper.Instance.SessionFactory.OpenSession());
//ISessionFactory GetSessionFactory(string? connectionString)
//{
//    FluentConfiguration configuration = Fluently
//        .Configure()
//        .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString)
//            .ShowSql()
//            .Driver<NHibernate.Driver.MicrosoftDataSqlClientDriver>()
//        );
//    configuration.ExposeConfiguration(x => x.SetProperty("hbm2ddl.keywords", "auto-quote"));
//    return configuration.BuildSessionFactory();
//}
//string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//ISessionFactory sessionFactory = GetSessionFactory(connectionString);
//builder.Services.AddSingleton(sessionFactory);
//builder.Services.AddScoped(factory => sessionFactory.OpenSession());

// POST XML from body.
builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
    //options.FormatterMappings.SetMediaTypeMappingForFormat("string", MediaTypeHeaderValue.Parse("text/plain"));
    options.FormatterMappings.SetMediaTypeMappingForFormat("string", MediaTypeHeaderValue.Parse("application/xml"));
    //options.FormatterMappings.SetMediaTypeMappingForFormat("XDocument", MediaTypeHeaderValue.Parse("application/xml"));
    //options.FormatterMappings.SetMediaTypeMappingForFormat("config", MediaTypeHeaderValue.Parse("application/xml"));
    //options.FormatterMappings.SetMediaTypeMappingForFormat("js", MediaTypeHeaderValue.Parse("application/json"));
}).AddXmlSerializerFormatters();
//GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer = true;

builder.Services.AddControllers();
builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Add(new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter());
}).AddXmlDataContractSerializerFormatters();

//builder.Services.AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
//    });
//bool prettyPrintJson = builder.Configuration.GetValue<string>("PrettyPrintJsonOutput") == "true";
//builder.Services.AddControllers(options =>
//{
//    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
//    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());

//    var jsonFormatter = options.OutputFormatters.OfType<SystemTextJsonOutputFormatter>().First();
//    jsonFormatter.SerializerOptions.WriteIndented = prettyPrintJson;
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Swagger documentaion.
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "WEB API Scales Documentaion",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

WebApplication app = builder.Build();
// Swagger documentaion.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
