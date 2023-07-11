using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Request;

namespace PersonalSoft.Service
{
    public interface IUsuarioService
    {
        Task<LoginResultDTO> ObtenerUsuarioPorLogin(LoginRequest request);
    }
}
