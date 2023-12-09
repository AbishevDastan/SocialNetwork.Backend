namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string Role { get; set; } = "Regular";
        public List<Post> Posts { get; set; }
    }
}
