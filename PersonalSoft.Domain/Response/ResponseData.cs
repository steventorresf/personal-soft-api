namespace PersonalSoft.Domain.Response
{
    public class ResponseData<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;

        public ResponseData(T data, string message)
        {
            Success = true;
            Data = data;
            Message = message;
        }
    }
}
