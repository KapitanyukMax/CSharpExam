namespace DataAccess.Entities
{
    public class User
    {
        public int CredentialsId { get; set; }

        public Credentials Credentials { get; set; }

        public Chat[] Chats { get; set; }
    }
}