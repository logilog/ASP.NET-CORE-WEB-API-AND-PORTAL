using System;

namespace Entities.REPOSITORY
{
    public class HaberDTO
    {
        public int? ID { get; set; }
        public string BASLIK { get; set; }
        public string OZET { get; set; }
        public string ICERIK { get; set; }
        public string GORSEL { get; set; }
        public string YAZAR { get; set; }
        public DateTime? TARIH { get; set; }
    }
}
