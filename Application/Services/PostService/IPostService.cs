using Application.UseCases.Post;
using Domain.Entities;

namespace Application.Services.PostService;

public interface IPostService
{
    Task<List<PostDto>> GetPosts();
    Task<PostDto> GetPost(int id);
    Task<List<PostDto>> GetPostsByUserId(int userId);
    Task<PostDto> AddPost(AddPostDto addPostDto);
    Task<PostDto> UpdatePost(UpdatePostDto updatePostDto, int id);
    Task<bool> DeletePost(int id);
}
