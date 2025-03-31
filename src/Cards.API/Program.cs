using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Swashbuckle.AspNetCore.SwaggerUI;
using Cards.API.Extensions;
using Cards.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
}

builder.Host.UseAppLoggerFor(builder.Environment.IsDevelopment(), builder.Configuration.GetValue<string>("ApiLogFilesFolder") ?? string.Empty);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers()
                .AddJsonOptions(x => { x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDependencyInjection();
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(ApplicationModuleMarker).Assembly); });
builder.Services.Configure<KestrelServerOptions>(x => x.AddServerHeader = false);

var app = builder.Build();

app.UseRouting();
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocExpansion(DocExpansion.None);
        c.SwaggerEndpoint("/swagger/v1/swagger.json", SwaggerTitleCardsApi);
    });
}

//app.UseHttpsRedirection();

app.MapControllers();
app.UseAppMiddlewares();

app.ExecuteActionsOnApplicationStartup();

app.Run();

