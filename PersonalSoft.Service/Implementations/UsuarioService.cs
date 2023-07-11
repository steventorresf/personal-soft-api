using Microsoft.IdentityModel.Tokens;
using PersonalSoft.Domain.DTO;
using PersonalSoft.Domain.Exceptions;
using PersonalSoft.Domain.Request;
using PersonalSoft.Domain.Settings;
using PersonalSoft.Entities;
using PersonalSoft.Persistence.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PersonalSoft.Service.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly Jwt _jwt;

        public UsuarioService(IUsuarioRepository repository, Jwt jwt)
        {
            _repository = repository;
            _jwt = jwt;
        }

        public async Task<LoginResultDTO> ObtenerUsuarioPorLogin(LoginRequest request)
        {
            Usuario? usuario = await _repository.ObtenerUsuarioByLogin(request.UserName, request.Password);
            if (usuario == null)
                throw new BadRequestException("El nombre de usuario y/o contraseña son incorrectos.");

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.GivenName, usuario.NombreCompleto),
                new Claim(ClaimTypes.Sid, usuario.Id)
            };

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SigningKey));

            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(signingCredentials);

            DateTime fechaExpiracion = DateTime.Now.AddMinutes(_jwt.ExpiredTimeMinutes);
            var payload = new JwtPayload(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: authClaims,
                notBefore: DateTime.Now,
                expires: fechaExpiracion);

            var token = new JwtSecurityToken(header, payload);

            return new LoginResultDTO
            {
                UsuarioId = usuario.Id,
                NombreCompleto = usuario.NombreCompleto,
                NombreUsuario = usuario.NombreUsuario,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                FechaExpiracion = fechaExpiracion
            };
        }
    }
}
