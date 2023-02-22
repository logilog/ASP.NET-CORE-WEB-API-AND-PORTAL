namespace Entities.General
{
    public class MiddlewareResult<T> : ResultModel<T> where T : class
    {
        public string ServiceMessage { get; set; }
        public MiddlewareResult(T Data) : base(Data)
        {
        }

        public MiddlewareResult(string Message, string ServiceMessage) : base(Message)
        {
            this.ServiceMessage = ServiceMessage;
        }

        public MiddlewareResult(bool Success) : base(Success)
        {
        }

        public MiddlewareResult(T Data, bool Success) : base(Data, Success)
        {
        }

        public MiddlewareResult(bool Success, string Message, string ServiceMessage) : base(Success, Message)
        {
            this.ServiceMessage = ServiceMessage;
        }
    }
}
