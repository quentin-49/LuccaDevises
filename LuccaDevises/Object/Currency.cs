namespace LuccaDevises.Object
{
    public class Currency
    {
        public decimal? Weight { get; set; } = null; // null egale valeur infinie
        public string Name { get; init; }
        public bool Marked { get; set; } = false;   // Device possiblement calculable
        public bool Processed { get; set; } = false;// Device calculé
        public string Predecessor { get; set; } = string.Empty; // 

        public Currency(string name, decimal? weight)
        {
            Weight = weight;
            Name = name;
        }
    }
}
