namespace Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTimeOffset CreationDate { get; set; }
        public List<PostReaction> PostReactions { get; set; } = new List<PostReaction>();
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
