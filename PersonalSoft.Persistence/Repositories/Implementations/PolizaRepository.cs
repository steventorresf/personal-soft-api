using MongoDB.Driver;
using MongoDB.Driver.Linq;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Entities;
using PersonalSoft.Entities.Response;

namespace PersonalSoft.Persistence.Repositories.Implementations
{
    public class PolizaRepository : IPolizaRepository
    {
        private readonly MongoDBContext _context;

        public PolizaRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateOne(Poliza poliza)
        {
            await _context.PolizaCollection.InsertOneAsync(poliza);
            return true;
        }

        public async Task<bool> UpdateOne(Poliza poliza)
        {
            FilterDefinition<Poliza> filter = Builders<Poliza>.Filter.Eq(x => x.Id, poliza.Id);
            UpdateDefinition<Poliza> update = Builders<Poliza>.Update.Set(po => po, poliza);
            await _context.PolizaCollection.FindOneAndUpdateAsync(filter, update);
            return true;
        }

        public async Task<long> GetLastNoPoliza()
        {
            return await _context.PolizaCollection.CountDocumentsAsync(FilterDefinition<Poliza>.Empty);
        }

        public async Task<bool> GetVigenciaByPlaca(string placaAutomotor)
        {
            DateTime fechaActual = DateTime.Now.Date;
            return await _context.PolizaCollection.AsQueryable()
                .AnyAsync(x => x.ClienteAutomotor.PlacaAutomotor.Equals(placaAutomotor) &&
                x.Vigencias.Any(y => y.FechaInicioVigencia >= fechaActual && y.FechaFinVigencia <= fechaActual));
        }

        public async Task<Poliza?> GetByPlacaAndPlanPoliza(string planPoliza, string placaAutomotor)
        {
            FilterDefinition<Poliza> filter = Builders<Poliza>.Filter.Eq(x => x.PlanPolizaId, planPoliza) & Builders<Poliza>.Filter.Eq(x => x.ClienteAutomotor.PlacaAutomotor, placaAutomotor);
            return await _context.PolizaCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task<ResponseListItem<ClienteAndPoliza>> GetByPlacaOrPoliza(PolizaByFiltersRequest request)
        {
            var query = (from po in _context.PolizaCollection.AsQueryable()
                         join cl in _context.ClienteCollection.AsQueryable() on po.ClienteAutomotor.ClienteId equals cl.Id
                         join pp in _context.PlanPolizaCollection.AsQueryable() on po.PlanPolizaId equals pp.Id
                         select new ClienteAndPoliza { Cliente = cl, PlanPoliza = pp, Poliza = po });

            if (!string.IsNullOrEmpty(request.Poliza))
                query = query.Where(x => x.Poliza.NoPoliza.Equals(request.Poliza));

            if (!string.IsNullOrEmpty(request.Placa))
                query = query.Where(x => x.Poliza.ClienteAutomotor.PlacaAutomotor.Equals(request.Placa));

            ResponseListItem<ClienteAndPoliza> response = new()
            {
                CountItems = await query.CountAsync().ConfigureAwait(false),
                ListItems = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync()
                .ConfigureAwait(false)
            };
            return response;
        }
    }
}
