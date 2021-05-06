using Blog.Core.Interfaces;
using Serilog;

namespace Blog.Infrastructure.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        public void LogInformation(string message, params object[] args)
        {
            Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
                .WriteTo.File("Information.txt").CreateLogger();

            Log.Information(message);
        }

        public void LogWarning(string message, params object[] args)
        {
            Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
                .WriteTo.File("Information.txt").CreateLogger();

            Log.Warning(message);
        }
    }
}
