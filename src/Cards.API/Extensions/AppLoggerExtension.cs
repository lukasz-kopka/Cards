using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Cards.API.Extensions;

public static class AppLoggerExtension
{
    private const string LogTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level}] {Message}{NewLine}{Exception}";
    private const string LogFileName = "Log.txt";

    public static void UseAppLoggerFor(this IHostBuilder hostBuilder, bool isDevelopment, string logFilesFolderConfig)
    {
        var now = DateTime.UtcNow;

        var targetLogFilesFolder = string.IsNullOrWhiteSpace(logFilesFolderConfig) ? AppDomain.CurrentDomain.BaseDirectory + "LogFiles" : logFilesFolderConfig;
        var baseLogConfiguration = new LoggerConfiguration().MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Serilog.AspNetCore", LogEventLevel.Warning);

        if (isDevelopment)
        {
            baseLogConfiguration = baseLogConfiguration
                .WriteTo.Console(theme: SystemConsoleTheme.Colored)
                .WriteTo.File(Path.Combine(targetLogFilesFolder, $"{now.Year}-{now.Month:d2}-{now.Day:d2}", LogFileName),
                    rollingInterval: RollingInterval.Infinite,
                    outputTemplate: LogTemplate);
        }
        else
        {
            baseLogConfiguration = baseLogConfiguration
                .WriteTo.File(Path.Combine(targetLogFilesFolder, LogFileName),
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: LogTemplate);
        }

        Log.Logger = baseLogConfiguration.CreateLogger();

        Log.Logger.Information($"SeriLog has been set up, use base folder for logging: {targetLogFilesFolder}");

        hostBuilder.UseSerilog();
    }
}
