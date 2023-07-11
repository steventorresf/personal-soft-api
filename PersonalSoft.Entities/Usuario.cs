using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PersonalSoft.Entities
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string NombreCompleto { get; set; } = string.Empty;

        public string NombreUsuario { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool Estado { get; set; }
    }
}
