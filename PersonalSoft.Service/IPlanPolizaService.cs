using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;

namespace PersonalSoft.Service
{
    public interface IPlanPolizaService
    {
        Task<bool> CreateMany(List<PlanPolizaPostDTO> planPolizas);
        Task<ResponseListItem<PlanPolizaDTO>> GetByFilters(PlanPolizaByFiltersRequest request);
    }
}
