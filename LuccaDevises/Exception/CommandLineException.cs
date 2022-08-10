namespace LuccaDevises.Exception
{
    public class CommandLineException : IOException
    {
        public CommandLineException(string message) : base(message)
        {
            Logger.LogMessage(LogLevel.Error, $"Error Level. Problème ligne de commande. -> {message}");
        }
    }
}
