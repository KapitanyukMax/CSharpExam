using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataAccess.Entities
{
    [Serializable]
    public class Chat
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public ICollection<UserChat> UsersChats { get; set; }

        public ICollection<Message> Messages { get; set; }

    }
}