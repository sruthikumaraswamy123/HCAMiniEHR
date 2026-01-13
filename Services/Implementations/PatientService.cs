using HCAMiniEHR.Models;
using HCAMiniEHR.Repositories.Interfaces;
using HCAMiniEHR.Services.Interfaces;

namespace HCAMiniEHR.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;

        public PatientService(IPatientRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task CreatePatientAsync(Patient patient)
        {
            await _repo.AddAsync(patient);
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            await _repo.UpdateAsync(patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<bool> IsDuplicateAsync(string fullName, string phone, int? excludeId = null)
        {
            return await _repo.ExistsAsync(fullName, phone, excludeId);
        }
    }
}
