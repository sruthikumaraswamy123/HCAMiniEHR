using HCAMiniEHR.Models;
using HCAMiniEHR.Repositories.Interfaces;
using HCAMiniEHR.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCAMiniEHR.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;

        public DoctorService(IDoctorRepository repo)
        {
            _repo = repo;
        }

        // Generic method names
        public Task<List<Doctor>> GetAllAsync() => _repo.GetAllDoctorsAsync();
        public Task<Doctor> GetByIdAsync(int id) => _repo.GetDoctorByIdAsync(id);
        public Task AddAsync(Doctor doctor) => _repo.AddDoctorAsync(doctor);
        public Task UpdateAsync(Doctor doctor) => _repo.UpdateDoctorAsync(doctor);
        public Task DeleteAsync(int id) => _repo.DeleteDoctorAsync(id);

        // Repository-style aliases (keeps compatibility with callers using these names)
        public Task<List<Doctor>> GetAllDoctorsAsync() => _repo.GetAllDoctorsAsync();
        public Task<Doctor> GetDoctorByIdAsync(int id) => _repo.GetDoctorByIdAsync(id);
        public Task AddDoctorAsync(Doctor doctor) => _repo.AddDoctorAsync(doctor);
        public Task UpdateDoctorAsync(Doctor doctor) => _repo.UpdateDoctorAsync(doctor);
        public Task DeleteDoctorAsync(int id) => _repo.DeleteDoctorAsync(id);
    }
}
