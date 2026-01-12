namespace HCAMiniEHR.DTOs
{
    public class PendingLabOrderDto
    {
        public int LabOrderId { get; set; }
        public string TestName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string PatientName { get; set; } = null!;
        public string DoctorName { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
    }
}
