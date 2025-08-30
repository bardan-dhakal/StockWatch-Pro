using System.Security.Claims;

namespace api.Extensions
{
    public static class ClaimsExtension
    {
        public static string? GetUserName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.GivenName)?.Value;
        }
    }
}