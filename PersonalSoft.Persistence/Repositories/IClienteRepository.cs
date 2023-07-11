using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Entities;

namespace PersonalSoft.Persistence.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente> GetById(string id);
        Task<bool> CreateMany(List<Cliente> clientes);
        Task<ResponseListItem<Cliente>> GetByFilters(ClienteByFiltersRequest request);
        Task<bool> UpdateOne(ClientePutDTO cliente);
    }
}
