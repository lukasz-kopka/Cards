namespace Cards.API.Extensions;

public static class ApplicationStartupExtension
{
    public static void ExecuteActionsOnApplicationStartup(this IApplicationBuilder src)
    {
        Task.Run(() =>
        {
            Log.Information($"[{nameof(ExecuteActionsOnApplicationStartup)}] - Star");
        });
    }
}
