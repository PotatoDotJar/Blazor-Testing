namespace BuildingComponents.Data.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public virtual ICollection<PostCategory> Categories { get; set; }
    }
}
