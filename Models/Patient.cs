using System.ComponentModel.DataAnnotations;
using HCAMiniEHR.ValidationAttributes;

namespace HCAMiniEHR.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name can only contain letters and spaces.")]
        public string FullName { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        [PastDate(ErrorMessage = "Date of Birth cannot be a future date.")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; } = string.Empty;

        // Navigation
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
