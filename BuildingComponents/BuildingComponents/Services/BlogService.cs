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

        public async Task<Post> CreatePostAsync(Post post, IEnumerable<PostCategory>? categories = null)
        {
            await _dbContext.Posts.AddAsync(post);

            if (categories?.Any() ?? false)
            {
                post.Categories = categories.ToList();
            }

            await _dbContext.SaveChangesAsync();

            return post;
        }

        public async Task<IEnumerable<Post>> GetBlogPostsAsync()
        {
            return await _dbContext.Posts.Include(x => x.Categories).ToListAsync();
        }

        public async Task<IEnumerable<Tuple<int, string>>> GetExistingCategoriesAsync()
        {
            return await _dbContext.Categories.Select(x => new Tuple<int, string>(x.CategoryId, x.CategoryName)).ToListAsync();
        }

        public async Task<PostCategory> AddOrGetCategoryByNameAsync(string name)
        {
            var existing = _dbContext.Categories.FirstOrDefault(x => x.CategoryName.Equals(name));
            if (existing == null)
            {
                existing = new PostCategory
                {
                    CategoryName = name
                };

                await _dbContext.Categories.AddAsync(existing);
                await _dbContext.SaveChangesAsync();
            }

            return existing;
        }

        public async Task<PostCategory?> GetCategoryByIdAsync(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        public async Task<Post?> GetPostAsync(int id)
        {
            return await _dbContext.Posts.FirstOrDefaultAsync(x => x.PostId == id);
        }

        public async Task<IEnumerable<PostCategory>> GetCategoryByIdsAsync(IEnumerable<int> ids)
        {
            return await _dbContext.Categories.Where(x => ids.Contains(x.CategoryId)).ToListAsync();
        }

        public async Task<Post?> UpdatePostAsync(int postId, Post updatedPost, IEnumerable<PostCategory>? categories = null)
        {
            var post = await _dbContext.Posts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.PostId == postId);

            if (post != null)
            {
                post.Title = updatedPost.Title;
                post.Content = updatedPost.Content;

                if (categories?.Any() ?? false)
                {
                    post.Categories = categories.ToList();
                }

                await _dbContext.SaveChangesAsync();

                return post;
            }

            return null;
        }

        public async Task DeletePostAsync(int postId)
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
