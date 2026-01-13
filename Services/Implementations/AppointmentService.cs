using HCAMiniEHR.Models;
using HCAMiniEHR.Repositories;
using HCAMiniEHR.Services.Interfaces;

namespace HCAMiniEHR.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;

        public AppointmentService(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Appointment>> GetAllAsync() => _repo.GetAllAsync();

        public Task AddAsync(Appointment appointment) => _repo.AddAsync(appointment);

        // Implementing methods based on interface
        public Task<Appointment?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public Task AddAppointmentAsync(Appointment appointment) => _repo.AddViaSpAsync(appointment);

        public Task UpdateAsync(Appointment appointment) => _repo.UpdateAsync(appointment);

        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

        public Task<List<Appointment>> GetByPatientIdAsync(int patientId) => _repo.GetByPatientIdAsync(patientId);
    }
}
