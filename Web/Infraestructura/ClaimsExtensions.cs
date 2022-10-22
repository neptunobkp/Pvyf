using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Infraestructura
{
    public static class ClaimsExtensions
    {
        public const string UsuarioIdClaimSchemaType = "usuarioid";
        public static string GetId(this ClaimsPrincipal principal) => principal.FindFirstValue(ClaimTypes.NameIdentifier);
        public static int GetUsuarioId(this ClaimsPrincipal principal) => Convert.ToInt32(principal.FindFirstValue(UsuarioIdClaimSchemaType));
    }
}
