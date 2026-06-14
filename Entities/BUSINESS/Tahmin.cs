using System;

namespace Entities.BUSINESS
{
    /// <summary>
    /// Bir kullanıcının bir maç için yaptığı skor tahmini.
    /// </summary>
    public class Tahmin
    {
        public int? ID { get; set; }
        public int? KULLANICIID { get; set; }
        public int? MACID { get; set; }
        public int? EVSAHIBISKOR { get; set; }
        public int? DEPLASMANSKOR { get; set; }
        public int? PUAN { get; set; }
        public DateTime? TAHMINTARIHI { get; set; }

        // Listeleme/gösterim için maç bilgileri (JOIN ile gelir)
        public string EVSAHIBIAD { get; set; }
        public string DEPLASMANAD { get; set; }
        public DateTime? MACTARIHI { get; set; }
        public string MACDURUM { get; set; }
        public int? GERCEKEVSKOR { get; set; }
        public int? GERCEKDEPSKOR { get; set; }
    }
}
