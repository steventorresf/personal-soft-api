namespace PersonalSoft.Domain.Request
{
    public class PolizaByFiltersRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? Placa { get; set; }
        public string? Poliza { get; set; }
    }
}
