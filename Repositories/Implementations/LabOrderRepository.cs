using HCAMiniEHR.Data.DbContext;
using HCAMiniEHR.Models;
using HCAMiniEHR.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCAMiniEHR.Repositories.Implementations
{
    public class LabOrderRepository : ILabOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public LabOrderRepository(ApplicationDbContext context) => _context = context;

        public async Task<List<LabOrder>> GetAllLabOrdersAsync()
            => await _context.LabOrders
                .Include(l => l.Appointment).ThenInclude(a => a!.Patient)
                .Include(l => l.Appointment).ThenInclude(a => a!.Doctor)
                .ToListAsync();

        public async Task<LabOrder?> GetLabOrderByIdAsync(int id)
            => await _context.LabOrders
                .Include(l => l.Appointment).ThenInclude(a => a!.Patient)
                .Include(l => l.Appointment).ThenInclude(a => a!.Doctor)
                .FirstOrDefaultAsync(l => l.LabOrderId == id);

        public async Task AddLabOrderAsync(LabOrder l) { _context.LabOrders.Add(l); await _context.SaveChangesAsync(); }
        public async Task UpdateLabOrderAsync(LabOrder l) { _context.LabOrders.Update(l); await _context.SaveChangesAsync(); }
        public async Task DeleteLabOrderAsync(int id) { var l = await _context.LabOrders.FindAsync(id); if (l != null) { _context.LabOrders.Remove(l); await _context.SaveChangesAsync(); } }
        public async Task<List<LabOrder>> GetLabOrdersByAppointmentIdAsync(int appointmentId)
            => await _context.LabOrders.Where(l => l.AppointmentId == appointmentId)
                .Include(l => l.Appointment).ThenInclude(a => a!.Patient)
                .Include(l => l.Appointment).ThenInclude(a => a!.Doctor)
                .ToListAsync();
    }
}
