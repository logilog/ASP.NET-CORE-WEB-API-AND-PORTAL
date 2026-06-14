using System;

namespace Entities.BUSINESS
{
    /// <summary>
    /// Bir kullanıcının satın aldığı aktif/pasif üyelik kaydı.
    /// </summary>
    public class Uyelik
    {
        public int? ID { get; set; }
        public int? KULLANICIID { get; set; }
        public string PAKETKOD { get; set; }
        public decimal? TUTAR { get; set; }
        public DateTime? BASLANGIC { get; set; }
        public DateTime? BITIS { get; set; }
        public string DURUM { get; set; }
    }
}
