using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HCAMiniEHR.Models
{
    public class LabOrder
    {
        [Key]
        public int LabOrderId { get; set; }

        // FK to Appointment
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; } = null!;

        public string TestName { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
    