namespace Entities.General
{
    public class ResultModel<T> where T : class
    {
        /// <summary>
        /// Başarılı durumda datanın result a verilmesi
        /// </summary>
        /// <param name="Data"></param>
        public ResultModel(T Data)
        {
            this.Data = Data;
            this.Success = true;
        }
        /// <summary>
        /// Oluşan durumlara göre datanın result a verilmesi
        /// </summary>
        /// <param name="Data"></param>
        public ResultModel(T Data, bool Success)
        {
            this.Data = Data;
            this.Success = Success;
        }
        /// <summary>
        /// Hata durumunda mesajın result a verilmesi
        /// </summary>
        /// <param name="Data"></param>
        public ResultModel(string Message)
        {
            this.Message = Message;
        }
        /// <summary>
        /// OLuşan durum karşısnda mesajın result a verilmesi
        /// </summary>
        /// <param name="Data"></param>
        public ResultModel(bool Success, string Message)
        {
            this.Success = Success;
            this.Message = Message;
        }
        /// <summary>
        /// Hata durumunda mesajın result a verilmesi
        /// </summary>
        /// <param name="Data"></param>
        public ResultModel(bool Success)
        {
            this.Success = Success;
        }
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
