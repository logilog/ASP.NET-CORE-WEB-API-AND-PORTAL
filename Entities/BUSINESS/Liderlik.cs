namespace Entities.BUSINESS
{
    /// <summary>
    /// Liderlik tablosu satırı (toplam puana göre sıralı kullanıcı).
    /// </summary>
    public class Liderlik
    {
        public int? SIRA { get; set; }
        public int? KULLANICIID { get; set; }
        public string KULLANICIADI { get; set; }
        public string ADSOYAD { get; set; }
        public int? TOPLAMPUAN { get; set; }
        public int? TAHMINSAYISI { get; set; }
        public int? TAMSKORSAYISI { get; set; }
    }
}
