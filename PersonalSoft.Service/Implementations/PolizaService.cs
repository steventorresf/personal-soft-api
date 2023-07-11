using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Exceptions;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Entities;
using PersonalSoft.Persistence.Repositories;

namespace PersonalSoft.Service.Implementations
{
    public class PolizaService : IPolizaService
    {
        private readonly IPolizaRepository _repository;
        private readonly IClienteRepository _repositoryCliente;

        public PolizaService(IPolizaRepository repository, IClienteRepository repositoryCliente)
        {
            _repository = repository;
            _repositoryCliente = repositoryCliente;
        }

        private async Task<string> GetNoPoliza()
        {
            long noPoliza = await _repository.GetLastNoPoliza() + 1;
            string sNoPoliza = noPoliza.ToString();
            while(sNoPoliza.Length < 6)
            {
                sNoPoliza = "0" + sNoPoliza;
            }
            return sNoPoliza;
        }

        public async Task<string> CreateOne(PolizaPostDTO request)
        {
            if (request.FechaTomaPoliza > request.FechaInicioVigencia)
                throw new BadRequestException("La fecha inicio vigencia NO puede ser menor que la fecha toma póliza.");

            if (request.FechaInicioVigencia < DateTime.Now.Date)
                throw new BadRequestException("La fecha inicio de vigencia NO puede ser menor que la fecha actual.");

            if (request.FechaFinVigencia < request.FechaInicioVigencia)
                throw new BadRequestException("La fecha fin de vigencia NO puede ser menor que la fecha inicio vigencia.");

            bool vigencia = await _repository.GetVigenciaByPlaca(request.PlacaAutomotor);
            if (vigencia)
                throw new BadRequestException("Ya existe una poliza vigente con esta misma placa '" + request.PlacaAutomotor + "'.");

            await _repositoryCliente.UpdateOne(request.Cliente);

            Poliza? poliza = await _repository.GetByPlacaAndPlanPoliza(request.PlanPolizaId, request.PlacaAutomotor);
            if (poliza == null)
            {
                poliza = new()
                {
                    NoPoliza = await GetNoPoliza(),
                    PlanPolizaId = request.PlanPolizaId,
                    ClienteAutomotor = new()
                    {
                        ClienteId = request.Cliente.Id,
                        PlacaAutomotor = request.PlacaAutomotor,
                        TieneInspeccion = request.TieneInspeccion,
                    },
                    Vigencias = new List<Vigencia>
                    {
                        new Vigencia
                        {
                            Id = Guid.NewGuid(), FechaTomaPoliza = request.FechaTomaPoliza,
                            FechaInicioVigencia = request.FechaInicioVigencia,
                            FechaFinVigencia = request.FechaFinVigencia
                        }
                    }
                };

                await _repository.CreateOne(poliza);
                return "La poliza '" + poliza.NoPoliza + "' ha sido creada correctamente";
            }

            poliza.Vigencias.Add(new Vigencia
            {
                Id = Guid.NewGuid(),
                FechaTomaPoliza = request.FechaTomaPoliza,
                FechaInicioVigencia = request.FechaInicioVigencia,
                FechaFinVigencia = request.FechaFinVigencia
            });

            await _repository.UpdateOne(poliza);
            return "La poliza '" + poliza.NoPoliza + "' ha sido actualizada correctamente";
        }

        public async Task<ResponseListItem<PolizaResultDTO>> GetByFilters(PolizaByFiltersRequest request)
        {
            var result = await _repository.GetByPlacaOrPoliza(request);
            ResponseListItem<PolizaResultDTO> response = new()
            {
                CountItems = result.CountItems,
                ListItems = result.ListItems.Select(x => new PolizaResultDTO
                {
                    NoPoliza = x.Poliza.NoPoliza,
                    NombrePlanPoliza = x.PlanPoliza.NombrePlan,
                    ValorMaximo = x.PlanPoliza.ValorMaximo,
                    Coberturas = x.PlanPoliza.Coberturas,
                    NombreCliente = x.Cliente.Nombre,
                    Identificacion = x.Cliente.Identificacion,
                    FechaNacimiento = x.Cliente.FechaNacimiento,
                    CiudadResidencia = x.Cliente.CiudadResidencia,
                    DireccionResidencia = x.Cliente.DireccionResidencia,
                    PlacaAutomotor = x.Poliza.ClienteAutomotor.PlacaAutomotor,
                    ModeloAutomotor = x.Cliente.Automotors.FirstOrDefault(y => y.PlacaAutomotor.Equals(x.Poliza.ClienteAutomotor.PlacaAutomotor))?.ModeloAutomotor ?? "",
                    TieneInspeccion = x.Poliza.ClienteAutomotor.TieneInspeccion,
                    FechaTomaPoliza = x.Poliza.Vigencias.Last().FechaTomaPoliza,
                    FechaInicioVigencia = x.Poliza.Vigencias.Last().FechaInicioVigencia,
                    FechaFinVigencia = x.Poliza.Vigencias.Last().FechaFinVigencia
                }).ToList()
            };
            return response;
        }
    }
}
