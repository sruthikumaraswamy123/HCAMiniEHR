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

        // Not in interface/repo yet but stub existed?
        // IAppointmentRepository doesn't have GetByDoctorIdAsync or GetAllAppointmentsWithDetailsAsync in my last view.
        // I should remove them or add to repo.
        // For now, removing unused methods to compile clean or matching Interface.
        // Interface IAppointmentService (Step 131) has:
        // GetAllAsync, GetByIdAsync, AddAsync, UpdateAsync, DeleteAsync.
        // It DOES NOT have AddAppointmentAsync or GetByPatientIdAsync?
        // Wait, Step 131:
        /*
        public interface IAppointmentService
        {
            Task<List<Appointment>> GetAllAsync();
            Task<Appointment?> GetByIdAsync(int id);
            Task AddAsync(Appointment appointment);
            Task UpdateAsync(Appointment appointment);
            Task DeleteAsync(int id);
        }
        */
        // I need to Update IAppointmentService to include AddAppointmentAsync (SP) and GetByPatientIdAsync.
        
    }
}
