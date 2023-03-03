using System.ComponentModel.DataAnnotations;

namespace BuildingComponents.Data.Entities
{
    public class PostCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public PostCategory()
        {
            Posts = new HashSet<Post>();
        }

        public override int GetHashCode()
        {
            return CategoryId.GetHashCode();
        }
    }
}
