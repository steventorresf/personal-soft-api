using Microsoft.AspNetCore.Mvc;
using PersonalSoft.Api.Filters;
using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Service;

namespace PersonalSoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(UserValidationFilter))]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet("byFilter")]
        public async Task<ActionResult<ResponseData<ResponseListItem<ClienteResultDTO>>>> GetByFilters([FromQuery] ClienteByFiltersRequest filters)
        {
            var response = await _service.GetByFilters(filters);
            return new ResponseData<ResponseListItem<ClienteResultDTO>>(response, "Registros Ok.");
        }

        [HttpPost]
        public async Task<ActionResult<ResponseData<bool>>> Post([FromBody] List<ClientePostDTO> request)
        {
            var response = await _service.CreateMany(request);
            return new ResponseData<bool>(response, "Los clientes han sido creados correctamente.");
        }
    }
}
