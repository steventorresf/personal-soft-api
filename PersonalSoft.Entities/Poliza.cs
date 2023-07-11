using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalSoft.Entities
{
    public class Poliza
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string NoPoliza { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string PlanPolizaId { get; set; } = string.Empty;

        public ClienteAutomotor ClienteAutomotor { get; set; } = new();

        public List<Vigencia> Vigencias { get; set; } = new();
    }

    public class ClienteAutomotor
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ClienteId { get; set; } = string.Empty;
        public string PlacaAutomotor { get; set; } = string.Empty;
        public bool TieneInspeccion { get; set; }
    }

    public class Vigencia
    {
        public Guid Id { get; set; }
        public DateTime FechaTomaPoliza { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
    }
}
