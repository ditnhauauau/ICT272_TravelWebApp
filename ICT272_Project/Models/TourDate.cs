using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ICT272_Project.Models
{
    public class TourDate
    {
        [Key]
        public int TourDateID { get; set; }
        [Required]
        [ForeignKey("TourPackage")]
        public int PackageID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime AvailableDate { get; set; }
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Available";
        public TourPackage TourPackage { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
