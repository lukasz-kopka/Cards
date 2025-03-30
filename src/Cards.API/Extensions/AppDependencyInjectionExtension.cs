using FluentValidation;
using Cards.API.Middleware;
using Cards.API.Validators;
using Cards.Application.Queries.Actions;
using Cards.Application.Services;

namespace Cards.API.Extensions;

public static class AppDependencyInjectionExtension
{
    public static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.AddSingleton(Log.Logger);

        services.AddScoped<IValidator<GetCardActions>, GetCardActionsValidator>();

        services.AddScoped<ICardService, CardService>();

        services.AddScoped<ExceptionHandlingMiddleware>();
        services.AddScoped<RemoveServerHeaderMiddleware>();
    }

    public static void UseAppMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<RemoveServerHeaderMiddleware>();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}