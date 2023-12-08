using Application.UseCases.Follow;
using Application.UseCases.User;
using Domain.Entities;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        Task<int> Register(User user, string password);
        Task<TokenModel> Login(string email, string password);
        Task<bool> FollowUser(int followerId, int followingId);
        Task<bool> UnfollowUser(int followerId, int followingId);
    }
}
