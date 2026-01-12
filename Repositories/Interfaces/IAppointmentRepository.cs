using HCAMiniEHR.Models;

namespace HCAMiniEHR.Repositories
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAsync();
        Task AddAsync(Appointment appointment);
        Task<int> AddViaSpAsync(Appointment appointment);
        Task<Appointment?> GetByIdAsync(int id);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id);
        Task<List<Appointment>> GetByPatientIdAsync(int patientId);
    }
}
