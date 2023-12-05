using Application.UseCases.User;
using Domain.Entities;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        Task<int> Register(User user, string password);
        Task<TokenModel> Login(string email, string password);
    }
}
