namespace Entities.BUSINESS
{
    /// <summary>
    /// Gol krallığı tablosu satırı.
    /// </summary>
    public class GolKrali
    {
        public int? SIRA { get; set; }
        public int? OYUNCUID { get; set; }
        public string OYUNCUAD { get; set; }
        public string TAKIMAD { get; set; }
        public string BAYRAK { get; set; }
        public int? GOLSAYISI { get; set; }
    }
}
