using HCAMiniEHR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCAMiniEHR.Services.Interfaces
{
    public interface ILabOrderService
    {
        Task<List<LabOrder>> GetAllAsync();
        Task<LabOrder> GetByIdAsync(int id);
        Task AddAsync(LabOrder labOrder);
        Task UpdateAsync(LabOrder labOrder);
        Task DeleteAsync(int id);
        Task<List<LabOrder>> GetByAppointmentIdAsync(int appointmentId);
    }
}
