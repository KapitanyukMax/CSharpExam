using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class FileMessage : Message
    {
        [Required]
        public string Url { get; set; }

        public string? Caption { get; set; }
    }
}