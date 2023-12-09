using Application.AuthenticationHandlers.HashManager;
using Application.AuthenticationHandlers.JwtManager;
using Application.Extensions.UserContext;
using Application.UseCases.Follow;
using Application.UseCases.User;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IHashManager hashManager, IJwtManager jwtManager, IUserContextService userContextService, IMapper mapper)
        {
            _userRepository = userRepository;
            _hashManager = hashManager;
            _jwtManager = jwtManager;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            if(users == null)
            {
                throw new Exception("Users not found.");
            }
            return _mapper.Map<List<UserDto>>(users);
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
                if (followerId != followingId)
                {
                    await _userRepository.FollowUser(followerId, followingId);
                    return true;
                }
                throw new Exception("You can not follow yourself.");
            }
            throw new Exception("You are already following this user.");
        }

        public async Task<bool> UnfollowUser(int followerId, int followingId)
        {
            followerId = _userContextService.GetCurrentUserId();

            if (await _userRepository.AreUsersFollowingEachOther(followerId, followingId))
            {
                if (followerId != followingId)
                {
                    await _userRepository.UnfollowUser(followerId, followingId);
                    return true;
                }
                throw new Exception("You can not unfollow yourself.");
            }
            throw new Exception("You can not unfollow the user, who is not followed by you.");
        }

        public async Task<List<UserDto>> SearchUsers(string searchText)
        {
            var result = await _userRepository.SearchUsers(searchText);

            if(result == null || result.Count <= 0)
                throw new InvalidOperationException("Users not found.");

            return _mapper.Map<List<UserDto>>(result);
        }
    }
}
