using HCAMiniEHR.Models;

namespace HCAMiniEHR.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<LabOrder>> GetPendingLabOrdersAsync();
        Task<List<Patient>> GetPatientsWithoutFollowUpAsync();
        Task<List<DoctorProductivityDto>> GetDoctorProductivityAsync();
    }
}
