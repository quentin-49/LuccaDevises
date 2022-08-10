using LuccaDevises.Exception;
using LuccaDevises.Ressource;

namespace LuccaDevises.Object
{
    public static class CurrencyChangeMethods
    {
        public static void AddDeviceChange(this List<CurrencyChange> deviceChanges, string startPoint, string endPoint, decimal weight)
        {
            if (weight == 0)
                throw new IntegrationDataException(Constant.DeviceChangeNullError);
            // Vérifier l'existence du couple 
            // Vérifier qu'il n'existe pas d'un couple inverse et incohérent
            if (deviceChanges.Any(s => s.StartPoint == startPoint && s.EndPoint == endPoint) ||
                deviceChanges.Any(s => s.StartPoint == endPoint && s.EndPoint == startPoint))
                throw new IntegrationDataException(Constant.DeviceChangeAlreadyExist);

            // Ajouter dans le liste
            deviceChanges.Add(new CurrencyChange(startPoint, endPoint, weight));
        }

        /// <summary>
        /// Récupère la liste des taux de change lié à la device passée en paramêtre
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<CurrencyChange> GetDeviceChangeFromName(this List<CurrencyChange> deviceChanges, string name)
        {
            return deviceChanges.Where(s => s.StartPoint == name)
                .Union(
                    deviceChanges.Where(s => s.EndPoint == name)
                                .Select(s => s.ReverseSegment()))
                .ToList();
        }
    }
}
