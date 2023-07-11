using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Entities;
using System.Text.RegularExpressions;

namespace PersonalSoft.Persistence.Repositories.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly MongoDBContext _context;

        public ClienteRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<Cliente> GetById(string id)
        {
            FilterDefinition<Cliente> filter = Builders<Cliente>.Filter.Eq(x => x.Id, id);
            return await _context.ClienteCollection.FindAsync(filter).Result.FirstAsync();
        }

        public async Task<bool> CreateMany(List<Cliente> clientes)
        {
            await _context.ClienteCollection.InsertManyAsync(clientes);
            return true;
        }

        public async Task<bool> UpdateOne(ClientePutDTO cliente)
        {
            FilterDefinition<Cliente> filter = Builders<Cliente>.Filter.Eq(x => x.Id, cliente.Id);
            UpdateDefinition<Cliente> update = Builders<Cliente>.Update
                .Set(cli => cli.Nombre, cliente.Nombre)
                .Set(cli => cli.Identificacion, cliente.Identificacion)
                .Set(cli => cli.CiudadResidencia, cliente.CiudadResidencia)
                .Set(cli => cli.DireccionResidencia, cliente.DireccionResidencia)
                .Set(cli => cli.FechaNacimiento, cliente.FechaNacimiento);
            await _context.ClienteCollection.UpdateOneAsync(filter, update);
            return true;
        }

        public async Task<ResponseListItem<Cliente>> GetByFilters(ClienteByFiltersRequest request)
        {
            FilterDefinition<Cliente> filter = FilterDefinition<Cliente>.Empty;
            if (!string.IsNullOrEmpty(request.Nombre))
                filter &= Builders<Cliente>.Filter.Regex(x => x.Nombre, BsonRegularExpression.Create(new Regex(request.Nombre, RegexOptions.IgnoreCase)));

            if(!string.IsNullOrEmpty(request.Identificacion))
                filter &= Builders<Cliente>.Filter.Regex(x => x.Identificacion, BsonRegularExpression.Create(new Regex(request.Identificacion, RegexOptions.IgnoreCase)));

            ResponseListItem<Cliente> response = new()
            {
                CountItems = await _context.ClienteCollection.CountDocumentsAsync(filter).ConfigureAwait(false),
                ListItems = await _context.ClienteCollection.Find(filter)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Limit(request.PageSize)
                .ToListAsync()
                .ConfigureAwait(false)
            };
            return response;
        }
    }
}
