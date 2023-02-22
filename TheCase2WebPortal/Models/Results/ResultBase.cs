namespace TheCase2WebPortal.Models.Results
{
    public class ResultBase<T> where T : class
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
