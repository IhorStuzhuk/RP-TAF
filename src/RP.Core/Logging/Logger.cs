using RP.Core.Logging;

namespace RP.Core.Logger
{
    public static class Logger
    {
        public static ITestLogger Log { get; }

        static Logger()
        {
            Log = LoggerFactory.GetLogger((LoggerType)Enum.Parse(typeof(LoggerType), 
                AppConfig.Instance.GetSection("Logger").Value, ignoreCase: true));
        }
    }
}
