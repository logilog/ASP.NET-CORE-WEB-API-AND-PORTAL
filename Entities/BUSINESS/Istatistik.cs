namespace Entities.BUSINESS
{
    /// <summary>
    /// Turnuva geneli istatistik özeti (hesaplanır).
    /// </summary>
    public class Istatistik
    {
        public int? TOPLAMMAC { get; set; }
        public int? OYNANANMAC { get; set; }
        public int? TOPLAMGOL { get; set; }
        public decimal? MACBASIGOL { get; set; }
        public int? TOPLAMTAKIM { get; set; }
        public int? TOPLAMOYUNCU { get; set; }
        public int? TOPLAMTAHMIN { get; set; }
        public string GOLKRALI { get; set; }
        public int? GOLKRALIGOL { get; set; }
    }
}
