using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;

namespace PersonalSoft.Service
{
    public interface IPolizaService
    {
        Task<string> CreateOne(PolizaPostDTO request);
        Task<ResponseListItem<PolizaResultDTO>> GetByFilters(PolizaByFiltersRequest request);
    }
}
