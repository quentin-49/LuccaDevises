using LuccaDevises.Exception;
using LuccaDevises.Ressource;

namespace LuccaDevises.Object
{
    public static class CurrencyChangeMethods
    {
        public static void AddCurrencyChange(this List<CurrencyChange> CurrencyChanges, string startPoint, string endPoint, decimal weight)
        {
            if (weight <= 0)
                throw new IntegrationDataException(Constant.CurrencyChangeNullError);
            // Vérifier l'existence du couple 
            // Vérifier qu'il n'existe pas d'un couple inverse et incohérent
            if (CurrencyChanges.Any(s => s.StartPoint == startPoint && s.EndPoint == endPoint) ||
                CurrencyChanges.Any(s => s.StartPoint == endPoint && s.EndPoint == startPoint))
                throw new IntegrationDataException(Constant.CurrencyChangeAlreadyExist);

            // Ajouter dans le liste
            CurrencyChanges.Add(new CurrencyChange(startPoint, endPoint, weight));
        }

        /// <summary>
        /// Récupère la liste des taux de change lié à la Currency passée en paramêtre
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<CurrencyChange> GetCurrencyChangeFromName(this List<CurrencyChange> CurrencyChanges, string name)
        {
            return CurrencyChanges.Where(s => s.StartPoint == name)
                .Union(
                    CurrencyChanges.Where(s => s.EndPoint == name)
                                .Select(s => s.ReverseSegment()))
                .ToList();
        }
    }
}
