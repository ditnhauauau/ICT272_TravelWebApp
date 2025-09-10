using System.ComponentModel.DataAnnotations;
namespace ICT272_Project.Models
{
    public class Tourist
    {
        [Key]
        public int TouristID { get; set; }
        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required, StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;
        [StringLength(20)]
        public string? ContactNumber { get; set; }
    }
}
