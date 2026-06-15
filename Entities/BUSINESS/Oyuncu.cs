namespace Entities.BUSINESS
{
    /// <summary>
    /// Bir milli takım oyuncusu (kadro).
    /// </summary>
    public class Oyuncu
    {
        public int? ID { get; set; }
        public int? TAKIMID { get; set; }
        public string AD { get; set; }
        public int? NUMARA { get; set; }
        public string MEVKI { get; set; }
        public string TAKIMAD { get; set; }
    }
}
