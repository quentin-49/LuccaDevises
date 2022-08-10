namespace LuccaDevises.Object
{
    public record Record : CurrencyChange
    {
        public Record(string startPoint, string endPoint, decimal weight) : base(startPoint, endPoint, weight)
        {
        }
    }
}
