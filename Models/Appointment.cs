using System;
using System.ComponentModel.DataAnnotations;

namespace HCAMiniEHR.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [Required]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        [Required]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        public string Reason { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "";
    }
}
