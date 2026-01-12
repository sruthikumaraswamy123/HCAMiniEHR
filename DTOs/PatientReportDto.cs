namespace HCAMiniEHR.DTOs
{
    public class PatientReportDto
    {
        public int PatientId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public int TotalAppointments { get; set; }
    }
}
