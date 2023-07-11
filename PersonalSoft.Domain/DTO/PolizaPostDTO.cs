namespace PersonalSoft.Domain.DTO
{
    public class PolizaPostDTO
    {
        public string PlanPolizaId { get; set; } = string.Empty;
        public ClientePutDTO Cliente { get; set; } = new();
        public string PlacaAutomotor { get; set; } = string.Empty;
        public bool TieneInspeccion { get; set; }
        public DateTime FechaTomaPoliza { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
    }
}
