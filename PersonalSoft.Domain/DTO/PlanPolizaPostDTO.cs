namespace PersonalSoft.Domain.DTO
{
    public class PlanPolizaPostDTO
    {
        public string NombrePlan { get; set; } = string.Empty;
        public List<string> Coberturas { get; set; } = new();
        public decimal ValorMaximo { get; set; }
    }
}
