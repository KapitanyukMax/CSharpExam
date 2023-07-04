namespace DataAccess.Entities
{
    [Serializable]
    public class Message
    {
        public int Id { get; set; }

        public DateTime SendingTime { get; set; }

        public string? Discriminator { get; set; }

        public int ChatId { get; set; }

        [NonSerialized]
        private Chat chat;

        public Chat Chat
        {
            get => chat;
            set => chat = value;
        }

        public int SenderId { get; set; }

        [NonSerialized]
        private User sender;

        public User Sender
        {
            get => sender;
            set => sender = value;
        }
    }
}