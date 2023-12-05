using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
