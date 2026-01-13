using HCAMiniEHR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCAMiniEHR.Services.Interfaces
{
    public interface IDoctorService
    {
        // existing, generic names
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int id);
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(int id);

        // repository-style method names used elsewhere in the codebase
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(int id);
        Task AddDoctorAsync(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(int id);
    }
}