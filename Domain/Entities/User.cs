namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public string Role { get; set; } = "Regular";
    }
}
