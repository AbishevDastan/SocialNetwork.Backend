using Domain.Enums;

namespace Domain.Entities
{
    public class PostReaction
    {
        public int Id { get; set; }
        public PostReactions ReactionType { get; set; }
        public int UserId { get; set; }
    }
}
