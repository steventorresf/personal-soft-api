using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Entities;

namespace PersonalSoft.Persistence.Repositories
{
    public interface IPlanPolizaRepository
    {
        Task<bool> CreateMany(List<PlanPoliza> planPolizas);
        Task<ResponseListItem<PlanPoliza>> GetByFilters(PlanPolizaByFiltersRequest request);
        Task CreateManyDefault();
    }
}
