using System;

namespace Application.UseCases.User
{
    public class TokenModel
    {
        public string Token { get; set; } = string.Empty;
        public DateTimeOffset ExpiresAt { get; set; }
    }
}
