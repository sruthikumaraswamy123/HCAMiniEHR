using HCAMiniEHR.Models;

namespace HCAMiniEHR.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int id);
        Task AddAsync(Appointment appointment);
        Task AddAppointmentAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(int id);
        Task<List<Appointment>> GetByPatientIdAsync(int patientId);
    }
}
