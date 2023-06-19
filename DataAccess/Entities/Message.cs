namespace DataAccess.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public int ChatId { get; set; }

        public Chat Chat { get; set; }
    }
}