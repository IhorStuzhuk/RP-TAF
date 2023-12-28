using RP.Core.Logger;

namespace RP.Core.Logging
{
    public static class LoggerFactory
    {
        public static ITestLogger GetLogger(LoggerType type)
        {
            return type switch
            {
                LoggerType.Console => new ConsoleLogger(),
                LoggerType.File => new FileLogger(),
                _ => throw new NotSupportedException($"Logger type {type} is not supported!"),
            };
        }
    }
}
