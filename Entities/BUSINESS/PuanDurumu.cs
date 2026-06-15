namespace Entities.BUSINESS
{
    /// <summary>
    /// Grup puan durumu satırı (oynanan maçlardan hesaplanır).
    /// </summary>
    public class PuanDurumu
    {
        public int? SIRA { get; set; }
        public string GRUP { get; set; }
        public int? TAKIMID { get; set; }
        public string TAKIMAD { get; set; }
        public string BAYRAK { get; set; }
        public int? OM { get; set; }       // oynanan maç
        public int? G { get; set; }        // galibiyet
        public int? B { get; set; }        // beraberlik
        public int? M { get; set; }        // mağlubiyet
        public int? AG { get; set; }       // attığı gol
        public int? YG { get; set; }       // yediği gol
        public int? AVERAJ { get; set; }
        public int? PUAN { get; set; }
    }
}
