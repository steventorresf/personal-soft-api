using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Entities;
using PersonalSoft.Entities.Response;

namespace PersonalSoft.Persistence.Repositories
{
    public interface IPolizaRepository
    {
        Task<bool> CreateOne(Poliza poliza);
        Task<bool> UpdateOne(Poliza poliza);
        Task<long> GetLastNoPoliza();
        Task<bool> GetVigenciaByPlaca(string placaAutomotor);
        Task<Poliza?> GetByPlacaAndPlanPoliza(string planPoliza, string placaAutomotor);
        Task<ResponseListItem<ClienteAndPoliza>> GetByPlacaOrPoliza(PolizaByFiltersRequest request);
    }
}
