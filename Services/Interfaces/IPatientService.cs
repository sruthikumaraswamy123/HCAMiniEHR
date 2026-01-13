using HCAMiniEHR.Models;

namespace HCAMiniEHR.Services.Interfaces
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int id);
        Task CreatePatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(int id);
        Task<bool> IsDuplicateAsync(string fullName, string phone, int? excludeId = null);
    }
}
