using DataAccess.Entities;

namespace CommandLibrary
{
    public class ChatCommand
    {
        public string Command { get; set; }

        public Message Message { get; set; }
    }
}