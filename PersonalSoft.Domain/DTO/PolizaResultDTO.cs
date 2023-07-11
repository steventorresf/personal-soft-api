namespace PersonalSoft.Domain.DTO
{
    public class PolizaResultDTO
    {
        public string NoPoliza { get; set; } = string.Empty;
        public string NombrePlanPoliza { get; set; } = string.Empty;
        public decimal ValorMaximo { get; set; }
        public List<string> Coberturas { get; set; } = new();
        public string NombreCliente { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string CiudadResidencia { get; set; } = string.Empty;
        public string DireccionResidencia { get; set; } = string.Empty;
        public string PlacaAutomotor { get; set; } = string.Empty;
        public string ModeloAutomotor { get; set; } = string.Empty;
        public bool TieneInspeccion { get; set; }
        public DateTime FechaTomaPoliza { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
    }
}
