using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Post> AddPost(Post post, int currentUserId)
        {
            var newPost = new Post
            {
                Content = post.Content,
                UserId = currentUserId,
                CreationDate = DateTimeOffset.UtcNow
            };

            await _dbContext.Posts.AddAsync(newPost);
            await _dbContext.SaveChangesAsync();

            return newPost;
        }

        public async Task<bool> DeletePost(int id)
        {
            var post = await GetPost(id);

            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _dbContext.Posts
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Post>> GetPosts()
        {
            return await _dbContext.Posts
                .OrderByDescending(p => p.CreationDate)
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsByUserId(int userId)
        {
            return await _dbContext.Posts
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<Post> UpdatePost(Post post, int id)
        {
            var dbPost = await GetPost(id);

            if (dbPost != null)
            {
                dbPost.Content = post.Content;
                dbPost.CreationDate = post.CreationDate;

                await _dbContext.SaveChangesAsync();

                return dbPost;
            }
            throw new ArgumentNullException(nameof(post));
        }
    }
}

