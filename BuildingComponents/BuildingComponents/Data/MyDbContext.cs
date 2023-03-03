using BuildingComponents.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BuildingComponents.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> Categories { get; set; }
        public DbSet<PostCategories> PostCategories { get; set; }

        public string DbPath { get; }

        private readonly ILogger<MyDbContext> _logger;

        public MyDbContext(ILogger<MyDbContext> logger)
        {
            _logger = logger;

            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            _logger.LogInformation($"Database Path is {path}");

            DbPath = Path.Join(path, "blogging.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
    }
}
