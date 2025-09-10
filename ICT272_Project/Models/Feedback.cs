using System.ComponentModel.DataAnnotations;
using System;
namespace ICT272_Project.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackID { get; set; }

        [Required]
        public int BookingID { get; set; }

        [Required]
        public int TouristID { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
