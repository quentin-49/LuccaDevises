namespace LuccaDevises.Object
{
    public record Request
    {
        public string StartCurrency { get; init; } = string.Empty;
        public string EndCurrency { get; init; } = string.Empty;
        public int Amount { get; init; }
        
        public Request()
        {
        }

        public Request(string data)
        {
            string[] datas = data.Split(";");

            StartCurrency = datas[0];
            Amount = Convert.ToInt32(datas[1]);
            EndCurrency = datas[2];
        }
    }
}
