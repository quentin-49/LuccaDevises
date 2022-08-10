namespace LuccaDevises.Exception
{
    public class IntegrationDataException : FormatException
    {
        public IntegrationDataException(string message) : base(message)
        {
            Logger.LogMessage(LogLevel.Error, $"Error Level. Problème dans l'intégration des données. -> {message}");
        }
    }
}
