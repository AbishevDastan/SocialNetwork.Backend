using Domain.Entities;
using Domain.Enums;

namespace Domain.Abstractions
{
    public interface IPostRepository
    {
        Task<List<Post>> GetPosts();
        Task<Post> GetPost(int id);
        Task<List<Post>> GetPostsByUserId(int userId);
        Task<Post> AddPost(Post post, int currentUserId);
        Task<Post> UpdatePost(Post post, int id);
        Task<bool> DeletePost(int id);
        Task AddPostReaction(int postId, PostReactions reactionType, int userId);
        Task<List<PostReaction>> GetPostReactionsByPostId(int postId);
    }
}
