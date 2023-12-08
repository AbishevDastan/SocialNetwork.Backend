using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Follow
{
    public class FollowDto
    {
        public int Id { get; set; }
        public int FollowingId { get; set; }
        public int FollowerId { get; set; }
        public DateTimeOffset FollowDate { get; set; }
    }
}
