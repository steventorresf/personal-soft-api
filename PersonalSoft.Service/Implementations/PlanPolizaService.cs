using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Entities;
using PersonalSoft.Persistence.Repositories;

namespace PersonalSoft.Service.Implementations
{
    public class PlanPolizaService : IPlanPolizaService
    {
        private readonly IPlanPolizaRepository _repository;

        public PlanPolizaService(IPlanPolizaRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateMany(List<PlanPolizaPostDTO> planPolizas)
        {
            List<PlanPoliza> plans = planPolizas.Select(x => new PlanPoliza
            {
                NombrePlan = x.NombrePlan,
                ValorMaximo = x.ValorMaximo,
                Coberturas = x.Coberturas
            }).ToList();

            return await _repository.CreateMany(plans);
        }

        public async Task<ResponseListItem<PlanPolizaDTO>> GetByFilters(PlanPolizaByFiltersRequest request)
        {
            var result = await _repository.GetByFilters(request);
            return new ResponseListItem<PlanPolizaDTO>
            {
                CountItems = result.CountItems,
                ListItems = result.ListItems.Select(x => new PlanPolizaDTO
                {
                    Id = x.Id,
                    NombrePlan = x.NombrePlan,
                    ValorMaximo = x.ValorMaximo,
                    Coberturas = x.Coberturas
                }).ToList()
            };
        }
    }
}
