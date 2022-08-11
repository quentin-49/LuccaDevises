using LuccaDevises.Exception;
using LuccaDevises.Ressource;
using System.Globalization;

namespace LuccaDevises.Object
{
    public sealed class Graph
    {
        public List<CurrencyChange> CurrencyChanges { get; set; }
        public List<Currency> Currencys { get; set; }
        public Request Request { get; set; }
        public List<Record> RecordList { get; set; }
        public int CurrencyNumber { get; set; }

        private static Graph? _instance;

        public static Graph GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Graph();
            }
            return _instance;
        }

        public Graph()
        {
            CurrencyChanges = new List<CurrencyChange>();
            Currencys = new List<Currency>();
            RecordList = new List<Record>();
            Request = new Request();
        }

        public void AddCurrencyChange(string data)
        {
            string[] datas = data.Split(";");

            decimal.TryParse(datas[2], NumberStyles.Number, CultureInfo.InvariantCulture, out decimal dec);

            AddCurrencyChange(datas[0], datas[1], dec);
        }

        private void AddCurrencyChange(string startPoint, string endPoint, decimal weight)
        {
            CurrencyChanges.AddCurrencyChange(startPoint, endPoint, weight);
        }

        /// <summary>
        /// Gènene la liste des Currencys présentes dans la liste
        /// </summary>
        /// <exception cref="IntegrationDataException"></exception>
        public void GenerateCurrencies()
        {
            if (CurrencyNumber != CurrencyChanges.Count)
                throw new IntegrationDataException(Constant.IncoherentCurrencyChanges);

            AddCurrencys(CurrencyChanges);
        }

        /// <summary>
        /// Génère les Currencys qui servirait de liste d'état.
        /// </summary>
        /// <returns></returns>
        public void AddCurrencys(List<CurrencyChange> CurrencyChangeList)
        {
            foreach (var point in CurrencyChangeList.Select(l => l.StartPoint).Union(CurrencyChangeList.Select(l => l.EndPoint)).Distinct().ToList())
            {
                Currencys.Add(new Currency(point, null));
            }
        }

        public Currency GetStartedCurrency()
        {
            Currency p = Currencys.First(p => p.Name == Request.StartCurrency);
            p.Weight = 1;
            p.Marked = true;
            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Currency"></param>
        public Currency? CurrencyProcessing(Currency Currency)
        {
            List<CurrencyChange> CurrencyChangeList = CurrencyChanges.GetCurrencyChangeFromName(Currency.Name);

            CalculCurrency(Currency, CurrencyChangeList);

            Currency.Processed = true;

            RegisterRecord(Currency);

            return GetMinCurrency();
        }

        /// <summary>
        /// Met à jour les Currencys approximité de celle passé en paramêtre
        /// </summary>
        /// <param name="Currency"></param>
        /// <param name="CurrencyChangeList"></param>
        private void CalculCurrency(Currency Currency, List<CurrencyChange> CurrencyChangeList)
        {
            foreach (var seg in CurrencyChangeList)
            {
                var CurrencyProcessing = Currencys.FirstOrDefault(p => p.Name == seg.EndPoint && !p.Processed);

                if (CurrencyProcessing == null)
                    continue;

                if (CurrencyProcessing.Weight == null || CurrencyProcessing.Weight > Currency.Weight * seg.Weight)
                {
                    CurrencyProcessing.Weight = Math.Round((Currency.Weight ?? 1) * seg.Weight, 4);
                    CurrencyProcessing.Predecessor = seg.StartPoint;
                }

                CurrencyProcessing.Marked = true;
            }
        }

        /// <summary>
        /// Retourne le résultat du programme en cherchant le point enregistré.
        /// </summary>
        /// <returns></returns>
        public decimal GetResult()
        {
            if (!RecordList.Any(r => r.StartPoint == Request.EndCurrency))
                throw new ResultException(Constant.NoResultCalculeted);

            return RecordList.Where(r => r.StartPoint == Request.EndCurrency).Select(r => Math.Ceiling(r.Weight * Request.Amount)).First();
        }

        /// <summary>
        /// Enregistre le chemin choisi pour la résolution finale
        /// </summary>
        /// <param name="pointRecord"></param>
        private void RegisterRecord(Currency pointRecord)
        {
            RecordList.Add(new Record(pointRecord.Name, pointRecord.Predecessor, pointRecord.Weight ?? 0));
        }

        /// <summary>
        /// Recherche la Currency calculé la plus faible
        /// Currency qui n'a pas encore été traité et qui dans le sous graph (Currency.Marked = 1)
        /// </summary>
        /// <returns></returns>
        private Currency? GetMinCurrency()
        {
            if (!Currencys.Any(pt => pt.Weight != 0 && !pt.Processed && pt.Marked))
                return null;

            return Currencys
                        .OrderBy(d => d.Weight)
                        .FirstOrDefault(d => d.Weight != 0
                            && d.Weight != null
                            && !d.Processed
                            && d.Marked);
        }
    }
}
