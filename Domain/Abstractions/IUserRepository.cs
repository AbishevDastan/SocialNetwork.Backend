using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<int> Register(User user, string password);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> SearchUsers(string searchText);
        Task FollowUser(int followerId, int followingId);
        Task UnfollowUser(int followerId, int followingId);
        Task<bool> AreUsersFollowingEachOther(int followerId, int followingId);
        Task<Follow> GetFollow(int followerId, int followingId);
    }
}
