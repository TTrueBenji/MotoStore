using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace MotoStore.Serilog_Context
{
    public static class SerilogExtensions
    {
        public static LoggerConfiguration AddCoreConfiguration(this LoggerConfiguration configuration)
        {
            configuration.MinimumLevel.Information();
            configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Information);
            configuration.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
            configuration.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);
            return configuration;
        }

        public static LoggerConfiguration AddConsoleConfiguration(this LoggerConfiguration configuration)
        {
            configuration.WriteTo.Console(
                theme: AnsiConsoleTheme.Code,
                outputTemplate:
                "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level}] [C: {CorrelationId}, R:{RequestId}] {Message:lj}{NewLine}{Exception}"
            );

            return configuration;
        }
    }
}