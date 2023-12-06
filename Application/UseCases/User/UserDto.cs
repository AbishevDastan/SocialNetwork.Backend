using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public string Role { get; set; } = "Regular";
    }
}
