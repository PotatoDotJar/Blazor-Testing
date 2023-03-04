using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingComponents.Data.Entities
{
    public class PostCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
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
