using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<int> Register(User user, string password);
        Task<User> GetUserByEmail(string email);
    }
}
