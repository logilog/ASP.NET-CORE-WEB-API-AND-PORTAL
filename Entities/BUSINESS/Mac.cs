using System;

namespace Entities.BUSINESS
{
    /// <summary>
    /// Dünya Kupası 2026 maçı (fikstür).
    /// </summary>
    public class Mac
    {
        public int? ID { get; set; }
        public int? EVSAHIBIID { get; set; }
        public int? DEPLASMANID { get; set; }
        public string EVSAHIBIAD { get; set; }
        public string DEPLASMANAD { get; set; }
        public string EVSAHIBIBAYRAK { get; set; }
        public string DEPLASMANBAYRAK { get; set; }
        public DateTime? TARIH { get; set; }
        public string GRUP { get; set; }
        public string ASAMA { get; set; }
        public string STADYUM { get; set; }
        public string SEHIR { get; set; }
        public int? EVSAHIBISKOR { get; set; }
        public int? DEPLASMANSKOR { get; set; }
        public string DURUM { get; set; }
        public int? DAKIKA { get; set; }
    }
}
