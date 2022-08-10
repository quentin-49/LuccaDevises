namespace LuccaDevises.Ressource
{
    public class Constant
    {
        public const string ArgCleCmd = "LuccaDevises";

        #region Error Message

        public const string CommandLineNumberError = "Le nombre d'argument passé n'est pas correct.";
        public const string FileDoesNotExist = "Pas de fichier au chemin indiqué et/ou pas du bon type.";
        public const string ArgNotExist = "Commande non existante";
        public const string NoMatchFormat = "La donnée ne correspond pas à un format connu {0}.";
        public const string DeviceChangeAlreadyExist = "Il existe déjà taux de change pour ces devices.";
        public static string IncoherentDeviceChanges = "Nombre de taux de change incohérent";
        public static string DeviceChangeNullError = "Impossible d'avoir un taux de change égale à zéro.";
        public static string NoResultCalculeted = "Pas de résultat calculé car qu'il n'y a pas de lien entre la device de départ et celle d'arrivée.";

        #endregion

        public const string RequestRegex = @"^[A-Z]{3}[;][0-9]+[;][A-Z]{3}$";
        public const string DeviceChangeNumberRegex = @"^[0-9]+$";
        public const string DeviceChangeRegex = @"^[A-Z]{3}[;][A-Z]{3}[;][0-9]+(\.[0-9]{0,4})?$";
        public const string Extension = ".TXT";

    }
}
