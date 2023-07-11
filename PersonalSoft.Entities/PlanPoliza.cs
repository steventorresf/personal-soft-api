using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PersonalSoft.Entities
{
    public class PlanPoliza
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string NombrePlan { get; set; } = string.Empty;

        public List<string> Coberturas { get; set; } = new();

        public decimal ValorMaximo { get; set; }
    }
}
