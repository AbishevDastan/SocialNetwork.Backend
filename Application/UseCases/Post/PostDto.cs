﻿using Domain.Enums;

namespace Application.UseCases.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTimeOffset CreationDate { get; set; }
        public int UserId { get; set; }
        public List<PostReactionDto> PostReactions { get; set; } = new List<PostReactionDto>();
    }
}
