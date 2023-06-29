namespace DataAccess.Entities
{
    public class Chat
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public UserChat[] UsersChats { get; set; }

        public Message[] Messages { get; set; }
    }
}