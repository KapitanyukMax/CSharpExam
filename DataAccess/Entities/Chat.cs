namespace DataAccess.Entities
{
    public class Chat
    {
        public int Id { get; set; }

        public User[] Users { get; set; }

        public Message[] Messages { get; set; }
    }
}