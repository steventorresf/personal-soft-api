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
    public class PlanPolizaController : ControllerBase
    {
        private readonly IPlanPolizaService _service;

        public PlanPolizaController(IPlanPolizaService service)
        {
            _service = service;
        }

        [HttpGet("byFilter")]
        public async Task<ActionResult<ResponseData<ResponseListItem<PlanPolizaDTO>>>> GetByFilters([FromQuery] PlanPolizaByFiltersRequest filters)
        {
            var response = await _service.GetByFilters(filters);
            return new ResponseData<ResponseListItem<PlanPolizaDTO>>(response, "Registros Ok.");
        }

        [HttpPost]
        public async Task<ActionResult<ResponseData<bool>>> Post([FromBody] List<PlanPolizaPostDTO> request)
        {
            var response = await _service.CreateMany(request);
            return new ResponseData<bool>(response, "Los planes de poliza han sido creados correctamente.");
        }
    }
}
