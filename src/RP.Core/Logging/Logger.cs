namespace RP.Core.Logger
{
    public class Logger
    {
        public ITestLogger Log;

        public Logger(ITestLogger logger)
        {
            Log = logger;
        }
    }
}
