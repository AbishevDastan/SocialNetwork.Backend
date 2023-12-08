using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<int> Register(User user, string password);
        Task<User> GetUserByEmail(string email);
        Task FollowUser(int followerId, int followingId);
        Task UnfollowUser(int followerId, int followingId);
        Task<bool> AreUsersFollowingEachOther(int followerId, int followingId);
        Task<Follow> GetFollow(int followerId, int followingId);
    }
}
