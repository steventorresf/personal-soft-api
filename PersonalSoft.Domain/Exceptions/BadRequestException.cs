namespace PersonalSoft.Domain.Exceptions
{
    [Serializable]
    public class BadRequestException :Exception
    {
        public BadRequestException() : base() { }
        public BadRequestException(string message) : base(message) { }
    }
}
