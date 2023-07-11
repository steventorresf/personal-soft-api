using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalSoft.Domain.DTO
{
    public class LoginResultDTO
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string UsuarioId { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime FechaExpiracion { get; set; }
    }
}
