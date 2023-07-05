using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess.Entities
{
    [Serializable]
    public class FileMessage : Message
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public byte[] FileData { get; set; }

        public string? Caption { get; set; }

        public override string ToString()
        {
            return Caption;
        }
    }
}