using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IPostRepository
    {
        Task<List<Post>> GetPosts();
        Task<Post> GetPost(int id);
        Task<Post> AddPost(Post post, int currentUserId);
        Task<Post> UpdatePost(Post post, int id);
        Task<bool> DeletePost(int id);
    }
}
