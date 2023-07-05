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
            return Text;
        }
    }
}