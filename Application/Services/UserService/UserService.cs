using Application.AuthenticationHandlers.HashManager;
using Application.AuthenticationHandlers.JwtManager;
using Application.Extensions.UserContext;
using Application.UseCases.Follow;
using Application.UseCases.User;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashManager _hashManager;
        private readonly IJwtManager _jwtManager;
        private readonly IUserContextService _userContextService;

        public UserService(IUserRepository userRepository, IHashManager hashManager, IJwtManager jwtManager, IUserContextService userContextService)
        {
            _userRepository = userRepository;
            _hashManager = hashManager;
            _jwtManager = jwtManager;
            _userContextService = userContextService;
        }

        public async Task<int> Register(User user, string password)
        {
            await _userRepository.Register(user, password);

            return user.Id;
        }

        public async Task<TokenModel> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new Exception("User with this e-mail not found.");
            }
            else if (!_hashManager.VerifyHash(password, user.Hash, user.Salt))
            {
                throw new Exception("The password is incorrect. Please, try again.");
            }
            else
            {
                return new TokenModel
                {
                    Token = _jwtManager.GenerateJwtToken(user),
                    ExpiresAt = DateTimeOffset.UtcNow.AddHours(12)
                };
            }
        }

        public async Task<bool> FollowUser(int followerId, int followingId)
        {
            followerId = _userContextService.GetCurrentUserId();

            if (!await _userRepository.AreUsersFollowingEachOther(followerId, followingId))
            {
                await _userRepository.FollowUser(followerId, followingId);  
                return true;
            }
            throw new Exception("You are already following this user.");
        }

        public async Task<bool> UnfollowUser(int followerId, int followingId)
        {
            followerId = _userContextService.GetCurrentUserId();

            if (await _userRepository.AreUsersFollowingEachOther(followerId, followingId))
            {
                await _userRepository.UnfollowUser(followerId, followingId);
                return true;
            }
            throw new Exception("You can not unfollow the user, who is not followed by you.");
        }
    }
}
