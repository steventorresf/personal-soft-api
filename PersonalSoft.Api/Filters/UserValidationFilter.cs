using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using PersonalSoft.Domain.Exceptions;
using PersonalSoft.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;

namespace PersonalSoft.Api.Filters
{
    public class UserValidationFilter : IActionFilter
    {
        private readonly Jwt _jwt;

        public UserValidationFilter(Jwt jwt)
        {
            _jwt = jwt;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedException("El token es requerido.");

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token.ToString().Replace("Bearer ", "")))
                throw new UnauthorizedException("El formato del token es incorrecto.");

            var jwtSecurityToken = handler.ReadJwtToken(token.ToString().Replace("Bearer ", ""));
            if (jwtSecurityToken.Payload.ValidTo < DateTime.UtcNow)
                throw new UnauthorizedException("Su sesión ha caducado, por favor inicie sesión nuevamente.");

            string tokenUid = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type.Contains("/sid"))?.Value ?? "";
            bool isAudienceValid = jwtSecurityToken.Audiences.Any(audience => audience.Equals(_jwt.Audience));
            
            if (string.IsNullOrEmpty(tokenUid) || !jwtSecurityToken.Issuer.Equals(_jwt.Issuer) || !isAudienceValid)
                throw new UnauthorizedException("El token y/o usuario es corrupto.");

            if (!ObjectId.TryParse(tokenUid, out ObjectId objectId))
                throw new UnauthorizedException("El usuario es invalido, por favor inicie sesión.");
        }
    }
}
