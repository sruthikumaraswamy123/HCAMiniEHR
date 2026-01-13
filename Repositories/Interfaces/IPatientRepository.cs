using HCAMiniEHR.Models;

public interface IPatientRepository
{
    Task<List<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(int id);
    Task AddAsync(Patient patient);
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(string fullName, string phone, int? excludeId = null);
}
