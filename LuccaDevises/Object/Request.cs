namespace LuccaDevises.Object
{
    public record Request
    {
        public string StartDevice { get; init; } = string.Empty;
        public string EndDevice { get; init; } = string.Empty;
        public int Amount { get; init; }
        
        public Request()
        {
        }

        public Request(string data)
        {
            string[] datas = data.Split(";");

            StartDevice = datas[0];
            Amount = Convert.ToInt32(datas[1]);
            EndDevice = datas[2];
        }
    }
}
