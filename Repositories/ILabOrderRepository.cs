using HCAMiniEHR.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCAMiniEHR.Repositories.Interfaces
{
    public interface ILabOrderRepository
    {
        Task<List<LabOrder>> GetAllLabOrdersAsync();
        Task<LabOrder?> GetLabOrderByIdAsync(int id);
        Task AddLabOrderAsync(LabOrder labOrder);
        Task UpdateLabOrderAsync(LabOrder labOrder);
        Task DeleteLabOrderAsync(int id);
        Task<List<LabOrder>> GetLabOrdersByAppointmentIdAsync(int appointmentId);
    }
}
