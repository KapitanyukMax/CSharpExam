using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    [Serializable]
    public class TextMessage : Message
    {
        [Required]
        public string Text { get; set; }

        public override string ToString()
        {
            string senderName = new MessengerDbContext().Messages.Include(m => m.Sender)
                                                        .FirstOrDefault(m => m.Id == Id)
                                                        .Sender.Name;
            return $"{senderName}: {Text}";
        }
    }
}