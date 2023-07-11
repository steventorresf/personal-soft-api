namespace PersonalSoft.Domain.DTO
{
    public class PlanPolizaDTO
    {
        public string Id { get; set; } = string.Empty;
        public string NombrePlan { get; set; } = string.Empty;
        public List<string> Coberturas { get; set; } = new();
        public decimal ValorMaximo { get; set; }
    }
}
