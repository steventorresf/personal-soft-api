using MongoDB.Driver;
using PersonalSoft.Domain.Settings;
using PersonalSoft.Entities;

namespace PersonalSoft.Persistence
{
    public class MongoDBContext
    {
        private readonly MongoCollections _mongoCollections;
        private readonly MongoClient _context;
        private readonly IMongoDatabase _database;

        public MongoDBContext(MongoSettings mongoSettings)
        {
            _mongoCollections = mongoSettings.MongoCollections;
            _context = new MongoClient(mongoSettings.ServerName);
            _database = _context.GetDatabase(mongoSettings.Database);
        }

        public IMongoCollection<Cliente> ClienteCollection
        {
            get { return _database.GetCollection<Cliente>(_mongoCollections.Cliente); }
        }

        public IMongoCollection<PlanPoliza> PlanPolizaCollection
        {
            get { return _database.GetCollection<PlanPoliza>(_mongoCollections.PlanPoliza); }
        }

        public IMongoCollection<Poliza> PolizaCollection
        {
            get { return _database.GetCollection<Poliza>(_mongoCollections.Poliza); }
        }

        public IMongoCollection<Usuario> UsuarioCollection
        {
            get { return _database.GetCollection<Usuario>(_mongoCollections.Usuario); }
        }
    }
}
