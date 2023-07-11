namespace PersonalSoft.Domain.Exceptions
{
    [Serializable]
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base() { }
        public UnauthorizedException(string message) : base(message) { }
    }
}
