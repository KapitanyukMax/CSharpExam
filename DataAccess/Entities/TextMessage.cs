using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class TextMessage : Message
    {
        [Required]
        public string Text { get; set; }
    }
}