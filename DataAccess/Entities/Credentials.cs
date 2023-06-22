namespace DataAccess.Entities
{
    public class Credentials
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? MailAddress { get; set; }

        public int? UserId { get; set; }

        public User? User { get; set; }
    }
}