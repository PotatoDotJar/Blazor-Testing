using BuildingComponents.Data;
using BuildingComponents.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildingComponents.Services
{
    public class BlogService
    {
        private readonly MyDbContext _dbContext;

        public BlogService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Post> CreatePost(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();

            return post;
        }

        public IEnumerable<Post> GetBlogPosts()
        {
            return _dbContext.Posts.Include(x => x.Categories);
        }

        public async Task<Post?> UpdatePost(Post updatedPost)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == updatedPost.PostId);

            if (post != null)
            {
                post.Title = updatedPost.Title;
                post.Content = updatedPost.Content;

                await _dbContext.SaveChangesAsync();

                return post;
            }

            return null;
        }

        public async Task DeletePost(int postId)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == postId);

            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
