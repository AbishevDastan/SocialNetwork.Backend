using Application.Extensions.UserContext;
using Application.UseCases.Post;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public PostService(IPostRepository postRepository, IMapper mapper, IUserContextService userContextService)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<PostDto> AddPost(AddPostDto addPostDto)
        {
            if (addPostDto == null)
                throw new ArgumentNullException(nameof(addPostDto), "Post cannot be null.");

            if (string.IsNullOrWhiteSpace(addPostDto.Content))
                throw new ArgumentException("Post's content cannot be empty or null.", nameof(addPostDto.Content));

            var addedPost = await _postRepository.AddPost(_mapper.Map<Post>(addPostDto), _userContextService.GetCurrentUserId());

            return _mapper.Map<PostDto>(addedPost);
        }

        public async Task<bool> DeletePost(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid post ID.", nameof(id));

            var post = await GetPost(id);

            if (post == null)
                throw new Exception("Post not found");

            if (post.UserId != _userContextService.GetCurrentUserId())
                throw new Exception("Access denied. You are not the post author.");

            await _postRepository.DeletePost(id);
            return true;
        }

        public async Task<PostDto> GetPost(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid post ID.", nameof(id));

            var post = await _postRepository.GetPost(id);

            if (post == null)
                throw new Exception($"Post with ID {id} not found");

            return _mapper.Map<PostDto>(post);
        }

        public async Task<List<PostDto>> GetPosts()
        {
            var posts = await _postRepository.GetPosts();

            if (posts == null || posts.Count <= 0)
                throw new Exception("Posts not found");

            return _mapper.Map<List<PostDto>>(posts);
        }

        public async Task<PostDto> UpdatePost(UpdatePostDto updatePostDto, int id)
        {
            var post = await _postRepository.GetPost(id);

            if (post.UserId == _userContextService.GetCurrentUserId())
            {
                if (updatePostDto == null)
                    throw new ArgumentNullException(nameof(updatePostDto), "Post cannot be null.");

                if (string.IsNullOrWhiteSpace(updatePostDto.Content))
                    throw new ArgumentException("Post's content cannot be empty or null.", nameof(updatePostDto.Content));

                var updatedPost = await _postRepository.UpdatePost(_mapper.Map<Post>(updatePostDto), id);

                return _mapper.Map<PostDto>(updatedPost);
            }
            throw new Exception("Access denied. You must be the author to edit a post.");
        }
    }
}
