namespace Entities.BUSINESS
{
    /// <summary>
    /// Satın alınabilir üyelik paketi (FREE / VIP / PRO) - gelir modeli.
    /// </summary>
    public class UyelikPaket
    {
        public int? ID { get; set; }
        public string KOD { get; set; }
        public string AD { get; set; }
        public string ACIKLAMA { get; set; }
        public decimal? AYLIKUCRET { get; set; }
        public string OZELLIKLER { get; set; }
    }
}
