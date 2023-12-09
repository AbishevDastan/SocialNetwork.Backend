using Application.AuthenticationHandlers.HashManager;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHashManager _hashManager;

        public UserRepository(ApplicationDbContext dbContext, IHashManager hashManager)
        {
            _dbContext = dbContext;
            _hashManager = hashManager;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(admin => admin.Email.ToLower() == email.ToLower());

            if (user == null)
            {
                throw new Exception("The e-mail is incorrect. Please, try again.");
            }

            return user;
        }

        public async Task<List<User>> SearchUsers(string searchText)
        {
            searchText = searchText.ToLower(); 

            return await _dbContext.Users
                .Where(u => u.Email.ToLower().Contains(searchText) ||
                            u.Name.ToLower().Contains(searchText) ||
                            u.Surname.ToLower().Contains(searchText))
                .ToListAsync();
        }

        public async Task<int> Register(User user, string password)
        {
            _hashManager.CreateHash(password, out byte[] hash, out byte[] salt);

            user.Hash = hash;
            user.Salt = salt;

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task FollowUser(int followerId, int followingId)
        {
            var follow = new Follow
            {
                FollowerId = followerId,
                FollowingId = followingId,
                FollowDate = DateTime.Now
            };

            _dbContext.Follows.Add(follow);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UnfollowUser(int followerId, int followingId)
        {
            var follow = await GetFollow(followerId, followingId);

            if (follow != null)
            {
                _dbContext.Follows.Remove(follow);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> AreUsersFollowingEachOther(int followerId, int followingId)
        {
            return await _dbContext.Follows
                .AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);
        }

        public async Task<Follow> GetFollow(int followerId, int followingId)
        {
            return await _dbContext.Follows
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);
        }

        //public async Task<int> GetFollowerCount(int userId)
        //{
        //    return await _dbContext.Follows
        //        .CountAsync(f => f.FollowingId == userId);
        //}

        //public async Task<int> GetFollowingCount(int userId)
        //{
        //    return await _dbContext.Follows
        //        .CountAsync(f => f.FollowerId == userId);
        //}
    }
}
