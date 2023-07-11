namespace PersonalSoft.Domain.Request
{
    public class ClienteByFiltersRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? Nombre { get; set; }
        public string? Identificacion { get; set; }
    }
}
