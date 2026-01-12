namespace HCAMiniEHR.DTOs
{
    public class DoctorProductivityDto
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = null!;
        public int TotalAppointments { get; set; }
        public int PendingLabOrders { get; set; }
    }
}
