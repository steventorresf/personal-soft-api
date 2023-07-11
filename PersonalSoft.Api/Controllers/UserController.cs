using Microsoft.AspNetCore.Mvc;
using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Response;
using PersonalSoft.Service;

namespace PersonalSoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UserController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseData<LoginResultDTO>>> ObtenerUsuarioPorLogin([FromBody] LoginRequest request)
        {
            var response = await _service.ObtenerUsuarioPorLogin(request);
            return new ResponseData<LoginResultDTO>(response, "Usuario logueado");
        }
    }
}
