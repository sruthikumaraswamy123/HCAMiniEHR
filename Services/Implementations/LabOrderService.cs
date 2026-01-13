using HCAMiniEHR.Models;
using HCAMiniEHR.Repositories.Interfaces;
using HCAMiniEHR.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCAMiniEHR.Services.Implementations
{
    public class LabOrderService : ILabOrderService
    {
        private readonly ILabOrderRepository _repo;

        public LabOrderService(ILabOrderRepository repo)
        {
            _repo = repo;
        }

        public Task<List<LabOrder>> GetAllAsync() => _repo.GetAllLabOrdersAsync();
        public Task<LabOrder?> GetByIdAsync(int id) => _repo.GetLabOrderByIdAsync(id);
        public Task AddAsync(LabOrder labOrder) => _repo.AddLabOrderAsync(labOrder);
        public Task UpdateAsync(LabOrder labOrder) => _repo.UpdateLabOrderAsync(labOrder);
        public Task DeleteAsync(int id) => _repo.DeleteLabOrderAsync(id);
        public Task<List<LabOrder>> GetByAppointmentIdAsync(int appointmentId) => _repo.GetLabOrdersByAppointmentIdAsync(appointmentId);
    }
}
