using MongoDB.Bson;
using MongoDB.Driver;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Entities;
using System.Text.RegularExpressions;

namespace PersonalSoft.Persistence.Repositories.Implementations
{
    public class PlanPolizaRepository : IPlanPolizaRepository
    {
        private readonly MongoDBContext _context;

        public PlanPolizaRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateMany(List<PlanPoliza> planPolizas)
        {
            await _context.PlanPolizaCollection.InsertManyAsync(planPolizas);
            return true;
        }

        public async Task CreateManyDefault()
        {
            long count = await _context.PlanPolizaCollection.CountDocumentsAsync(FilterDefinition<PlanPoliza>.Empty);
            if(count == 0)
            {
                List<PlanPoliza> planPolizas = new()
                {
                    new PlanPoliza { NombrePlan = "Plan premium", ValorMaximo = 300000000, Coberturas = new() {"Todo riesgo", "Daño a terceros"}},
                    new PlanPoliza { NombrePlan = "Plan intermedio", ValorMaximo = 200000000, Coberturas = new() {"Deducible unico", "Daño a terceros"}},
                    new PlanPoliza { NombrePlan = "Plan basico", ValorMaximo = 100000000, Coberturas = new() {"Deducible salario minimo", "Daño a terceros"}},
                };
                await _context.PlanPolizaCollection.InsertManyAsync(planPolizas);
            }
        }

        public async Task<ResponseListItem<PlanPoliza>> GetByFilters(PlanPolizaByFiltersRequest request)
        {
            FilterDefinition<PlanPoliza> filter = FilterDefinition<PlanPoliza>.Empty;
            if (!string.IsNullOrEmpty(request.NombrePlan))
                filter &= Builders<PlanPoliza>.Filter.Regex(x => x.NombrePlan, BsonRegularExpression.Create(new Regex(request.NombrePlan, RegexOptions.IgnoreCase)));

            if (request.ValorMaximo > 0)
                filter &= Builders<PlanPoliza>.Filter.Eq(x => x.ValorMaximo, request.ValorMaximo);

            ResponseListItem<PlanPoliza> response = new()
            {
                CountItems = await _context.PlanPolizaCollection.CountDocumentsAsync(filter).ConfigureAwait(false),
                ListItems = await _context.PlanPolizaCollection.Find(filter)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Limit(request.PageSize)
                .ToListAsync()
                .ConfigureAwait(false)
            };
            return response;
        }
    }
}
