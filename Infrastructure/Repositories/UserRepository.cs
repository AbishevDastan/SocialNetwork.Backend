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
        public async Task<int> Register(User user, string password)
        {
            _hashManager.CreateHash(password, out byte[] hash, out byte[] salt);
            user.Hash = hash;
            user.Salt = salt;
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }
    }
}
