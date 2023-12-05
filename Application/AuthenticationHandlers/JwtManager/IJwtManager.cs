using Domain.Entities;

namespace Application.AuthenticationHandlers.JwtManager
{
    public interface IJwtManager
    {
        string GenerateJwtToken(User user);
    }
}
