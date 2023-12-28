using Serilog;

namespace RP.Core.Logger
{
    public class ConsoleLogger : ITestLogger
    {
        private readonly ILogger logger;

        public ConsoleLogger()
        {
            logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }

        public void Info(string line) => logger.Information(line);

        public void Debug(string line) => logger.Debug(line);

        public void Warn(string line) => logger.Warning(line);

        public void Error(string line) => logger.Error(line);
    }
}
