using MongoDB.Driver;
using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Entities;
using PersonalSoft.Persistence.Repositories;

namespace PersonalSoft.Service.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateMany(List<ClientePostDTO> request)
        {
            List<Cliente> clientes = request.Select(x => new Cliente
            {
                Nombre = x.Nombre,
                Identificacion = x.Identificacion,
                CiudadResidencia = x.CiudadResidencia,
                DireccionResidencia = x.DireccionResidencia,
                FechaNacimiento = x.FechaNacimiento,
                Automotors = x.Automotors.Select(y => new Automotor
                {
                    PlacaAutomotor = y.PlacaAutomotor,
                    ModeloAutomotor = y.ModeloAutomotor
                }).ToList()
            }).ToList();

            return await _repository.CreateMany(clientes);
        }

        public async Task<bool> UpdateOne(ClientePutDTO request)
        {
            return await _repository.UpdateOne(request);
        }

        public async Task<ResponseListItem<ClienteResultDTO>> GetByFilters(ClienteByFiltersRequest request)
        {
            var result = await _repository.GetByFilters(request);
            ResponseListItem<ClienteResultDTO> response = new()
            {
                CountItems = result.CountItems,
                ListItems = result.ListItems.Select(x => new ClienteResultDTO
                {
                    Id = x.Id,
                    Identificacion = x.Identificacion,
                    Nombre = x.Nombre,
                    CiudadResidencia = x.CiudadResidencia,
                    DireccionResidencia = x.DireccionResidencia,
                    FechaNacimiento = x.FechaNacimiento,
                    Automotors = x.Automotors.Select(y => new AutomotorDTO
                    {
                        PlacaAutomotor = y.PlacaAutomotor,
                        ModeloAutomotor = y.ModeloAutomotor
                    }).ToList()
                }).ToList()
            };
            return response;
        }
    }
}
