using Domain.Enums;

namespace Application.UseCases.Post
{
    public class PostReactionDto
    {
        public int Id { get; set; }
        public PostReactions ReactionType { get; set; }
        public int UserId { get; set; }
    }
}
