using RP.Core.Logger;
using Serilog;

namespace RP.Core.Logging
{
    public class FileLogger : ITestLogger
    {
        private const string LOG_FILE_PATH = "logs/logs.txt";

        private readonly ILogger logger;

        public FileLogger()
        {
            if(File.Exists(LOG_FILE_PATH))
                File.Delete(LOG_FILE_PATH);

            logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(LOG_FILE_PATH)
                .CreateLogger();
        }

        public void Info(string line) => logger.Information(line);

        public void Debug(string line) => logger.Debug(line);

        public void Warn(string line) => logger.Warning(line);

        public void Error(string line) => logger.Error(line);

        public void Stop() => ((IDisposable)logger).Dispose();
    }
}
