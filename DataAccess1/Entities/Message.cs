﻿namespace DataAccess.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public DateTime SendingTime { get; set; }

        public string? Discriminator { get; set; }

        public int ChatId { get; set; }

        public Chat Chat { get; set; }

        public int SenderId { get; set; }

        public User Sender { get; set; }
    }
}