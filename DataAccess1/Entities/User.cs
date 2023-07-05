namespace DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? MailAddress { get; set; }

        public ICollection<UserChat> UsersChats { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}