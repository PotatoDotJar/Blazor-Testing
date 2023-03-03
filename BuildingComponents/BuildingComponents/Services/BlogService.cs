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

        public async Task<Post> CreatePost(Post post, IEnumerable<PostCategory>? categories = null)
        {
            await _dbContext.Posts.AddAsync(post);

            if (categories?.Any() ?? false)
            {
                post.Categories = categories.ToList();
            }

            await _dbContext.SaveChangesAsync();

            return post;
        }

        public IEnumerable<Post> GetBlogPosts()
        {
            return _dbContext.Posts.Include(x => x.Categories);
        }

        public IEnumerable<Tuple<int, string>> GetExistingCategories()
        {
            return _dbContext.Categories.Select(x => new Tuple<int, string>(x.CategoryId, x.CategoryName));
        }

        public async Task<PostCategory> AddOrGetCategoryByName(string name)
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

        public async Task<PostCategory> GetCategoryById(int id)
        {
            return await _dbContext.Categories.FirstAsync(x => x.CategoryId == id);
        }

        public IEnumerable<PostCategory> GetCategoryByIds(IEnumerable<int> ids)
        {
            return _dbContext.Categories.Where(x => ids.Contains(x.CategoryId));
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
