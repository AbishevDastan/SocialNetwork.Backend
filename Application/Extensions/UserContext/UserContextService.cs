using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace Application.Extensions.UserContext
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private IIdentity? UserIdentity => _httpContextAccessor.HttpContext?.User.Identity;

        public int GetCurrentUserId()
        {
            var nameClaim = UserIdentity?.Name;
            if (!string.IsNullOrEmpty(nameClaim) && int.TryParse(nameClaim, out var userId))
            {
                return userId;
            }

            throw new ApplicationException("User is not authenticated!");
        }

        public bool IsUserLoggedIn()
        {
            return UserIdentity!.IsAuthenticated;
        }
    }
}
