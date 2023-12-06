﻿using Domain.Abstractions;
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

        public async Task<Post> AddPost(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();

            return post;
        }

        public async Task<bool> DeletePost(int id)
        {
            var post = GetPost(id);

            _dbContext.Remove(post);
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
                .ToListAsync();
        }

        public async Task<Post> UpdatePost(Post post, int id)
        {
            var dbPost = await GetPost(id);

            if (dbPost != null)
            {
                dbPost.Content = post.Content;
                dbPost.CreationDate = post.CreationDate;
                dbPost.UpdateDate = post.UpdateDate;
                dbPost.UserId = post.UserId;

                await _dbContext.SaveChangesAsync();

                return dbPost;
            }
            throw new ArgumentNullException(nameof(post));
        }
    }
}
}