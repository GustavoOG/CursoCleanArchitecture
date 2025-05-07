using System.Security.Claims;

namespace CleanArchitecture.Infrastructure.Authentication
{
    internal static class ClaimsPrincipalExtensions
    {
        public static string GetUserEmail(this ClaimsPrincipal? claimsPrincipal)
        {
            return claimsPrincipal?.FindFirstValue(ClaimTypes.Email)
                ?? throw new ApplicationException("Email claim not found");
        }

        public static Guid GetUserId(this ClaimsPrincipal? claimsPrincipal)
        {
            var userId = claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(userId, out var id)
                ? id
                : throw new ApplicationException("UserId claim not found");
        }
    }
}
