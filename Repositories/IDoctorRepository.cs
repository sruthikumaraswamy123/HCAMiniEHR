using HCAMiniEHR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCAMiniEHR.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(int id);
        Task AddDoctorAsync(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(int id);
    }
}
