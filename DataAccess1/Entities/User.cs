namespace DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string IPAddress { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? MailAddress { get; set; }

        public UserChat[] UsersChats { get; set; }

        public Message[] Messages { get; set; }
    }
}