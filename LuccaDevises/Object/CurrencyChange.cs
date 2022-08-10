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

        public decimal InverseWeight()
        {
            return Math.Round(1 / Weight, 4);
        }

        public CurrencyChange ReverseSegment()
        {
            return new CurrencyChange(EndPoint, StartPoint, weight: InverseWeight());
        }
    }
}
