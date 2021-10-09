using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetUserId(this ClaimsPrincipal principal)
        {
            var claim = principal.Claims.First(c => c.Type == CustomClaimTypes.Id);
            return long.Parse(claim.Value);
        }
    }
}
