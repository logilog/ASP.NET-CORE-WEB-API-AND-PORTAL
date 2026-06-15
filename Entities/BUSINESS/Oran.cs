namespace Entities.BUSINESS
{
    /// <summary>
    /// Bir maçın iddaa oranları (MS 1-X-2, Alt/Üst 2.5, Karşılıklı Gol).
    /// </summary>
    public class Oran
    {
        public int? ID { get; set; }
        public int? MACID { get; set; }
        public decimal? MS1 { get; set; }
        public decimal? MSX { get; set; }
        public decimal? MS2 { get; set; }
        public decimal? ALT25 { get; set; }
        public decimal? UST25 { get; set; }
        public decimal? KGVAR { get; set; }
        public decimal? KGYOK { get; set; }
    }
}
