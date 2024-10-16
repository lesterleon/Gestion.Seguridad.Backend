using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Gestion.Seguridad.Backend.Domain.Helpers
{
    public class SerilogHelper
    {
        public ILogger Logger { get; set; } = null!;

        public void JsonFileConfiguration(string path, LogEventLevel level, RollingInterval interval)
        {
            Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Override("Microsoft", level)
                .WriteTo.File(new JsonFormatter(), path, rollingInterval: interval)
                .CreateLogger();
        }
    }
}
