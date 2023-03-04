using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingComponents.Data.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Content { get; set; }

        public virtual ICollection<PostCategory> Categories { get; set; }

        public Post()
        {
            Categories = new HashSet<PostCategory>();
        }
    }
}
