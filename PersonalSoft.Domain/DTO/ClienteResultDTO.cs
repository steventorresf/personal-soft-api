namespace PersonalSoft.Domain.DTO
{
    public class ClienteResultDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string CiudadResidencia { get; set; } = string.Empty;
        public string DireccionResidencia { get; set; } = string.Empty;
        public List<AutomotorDTO> Automotors { get; set; } = new();
    }
}
