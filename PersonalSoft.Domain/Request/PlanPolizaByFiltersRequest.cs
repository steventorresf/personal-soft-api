namespace PersonalSoft.Domain.Request
{
    public class PlanPolizaByFiltersRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? NombrePlan { get; set; }
        public decimal ValorMaximo { get; set; }
    }
}
