using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICT272_Project.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }
        [Required]
        [ForeignKey("Tourist")]
        public int TouristID { get; set; }
        [Required]
        [ForeignKey("TourDate")]
        public int TourDateID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending";
        [Required]
        [StringLength(20)]
        public string PaymentStatus { get; set; } = "Unpaid";
        public Tourist Tourist { get; set; }
        public TourDate TourDate { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
