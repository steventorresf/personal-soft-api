namespace PersonalSoft.Domain.Response
{
    public class ResponseListItem<T>
    {
        public IEnumerable<T> ListItems { get; set; } = new List<T>();
        public long CountItems { get; set; }
    }
}
