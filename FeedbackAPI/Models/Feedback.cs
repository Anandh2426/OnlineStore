using System.ComponentModel.DataAnnotations;

namespace FeedbackAPI.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Rating { get; set; }
    }
}
