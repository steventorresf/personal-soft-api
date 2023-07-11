using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;

namespace PersonalSoft.Service
{
    public interface IClienteService
    {
        Task<bool> CreateMany(List<ClientePostDTO> request);
        Task<bool> UpdateOne(ClientePutDTO request);
        Task<ResponseListItem<ClienteResultDTO>> GetByFilters(ClienteByFiltersRequest request);
    }
}
