namespace LuccaDevises.Exception
{
    public class ResultException : IOException
    {
        public ResultException(string message) : base(message)
        {
            Logger.LogMessage(LogLevel.Error, $"Error Level. Problème dans la résolution du problème. -> {message}");
        }
    }
}
