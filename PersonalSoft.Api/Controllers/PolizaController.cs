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
    public class PolizaController : ControllerBase
    {
        private readonly IPolizaService _service;

        public PolizaController(IPolizaService service)
        {
            _service = service;
        }

        [HttpGet("byFilter")]
        public async Task<ActionResult<ResponseData<ResponseListItem<PolizaResultDTO>>>> GetByFilters([FromQuery] PolizaByFiltersRequest filters)
        {
            var response = await _service.GetByFilters(filters);
            return new ResponseData<ResponseListItem<PolizaResultDTO>>(response, "Registros Ok.");
        }

        [HttpPost]
        public async Task<ActionResult<ResponseData<string>>> Post([FromBody] PolizaPostDTO request)
        {
            var response = await _service.CreateOne(request);
            return new ResponseData<string>(response, response);
        }
    }
}
