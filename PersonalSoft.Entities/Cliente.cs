using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PersonalSoft.Entities
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Identificacion { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        public string CiudadResidencia { get; set; } = string.Empty;

        public string DireccionResidencia { get; set; } = string.Empty;

        public List<Automotor> Automotors { get; set; } = new();
    }

    public class Automotor
    {
        public string PlacaAutomotor { get; set; } = string.Empty;

        public string ModeloAutomotor { get; set; } = string.Empty;
    }
}
