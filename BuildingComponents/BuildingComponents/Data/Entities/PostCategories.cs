using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingComponents.Data.Entities
{
    public class PostCategories
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        [Required]
        [ForeignKey(nameof(PostCategory))]
        public int CategoryId { get; set; }
        public virtual PostCategory Category { get; set; }
    }
}
