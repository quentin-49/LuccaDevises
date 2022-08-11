namespace LuccaDevises.Object
{
    public record CurrencyChange
    {
        public string StartPoint { get; init; } = string.Empty;
        public string EndPoint { get; init; } = string.Empty;
        public decimal Weight { get; init; }

        public CurrencyChange(string startPoint, string endPoint, decimal weight)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Weight = weight;
        }

        /// <summary>
        /// Inverse la valeur du taux de change
        /// </summary>
        /// <returns></returns>
        public decimal InverseWeight()
        {
            return Math.Round(1 / Weight, 4);
        }

        /// <summary>
        /// Obtient une instance du taux de change "inversé"
        /// </summary>
        /// <returns></returns>
        public CurrencyChange ReverseSegment()
        {
            return new CurrencyChange(EndPoint, StartPoint, weight: InverseWeight());
        }
    }
}
