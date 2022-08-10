using LuccaDevises.Logger;

namespace LuccaDevises.Exception
{
    public enum LogLevel
    {
        Off,
        Critical,
        Error,
        Warning,
        Information,
        Trace
    }

    public class Logger
    {
        public static void LogMessage(LogLevel level, LogInterpolatedStringHandler builder)
        {
            if (LogLevel.Information < level) return;
            builder.GetFormattedText();
        }
    }
}
