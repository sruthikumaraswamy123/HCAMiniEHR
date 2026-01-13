using HCAMiniEHR.Data.DbContext;
using HCAMiniEHR.Models;
using HCAMiniEHR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HCAMiniEHR.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LabOrder>> GetPendingLabOrdersAsync()
        {
            return await _context.LabOrders
                .Include(lo => lo.Appointment)
                    .ThenInclude(a => a!.Patient)
                .Where(lo => lo.Status == "Pending")
                .ToListAsync();
        }

        public async Task<List<Patient>> GetPatientsWithoutFollowUpAsync()
        {
            // Patients with no future appointments
            var today = DateTime.Today;
            return await _context.Patients
                .Where(p => !p.Appointments.Any(a => a.AppointmentDate > today))
                .ToListAsync();
        }

        public async Task<List<DoctorProductivityDto>> GetDoctorProductivityAsync()
        {
            return await _context.Appointments
                .GroupBy(a => a.Doctor != null ? a.Doctor.Name : "Unknown")
                .Select(g => new DoctorProductivityDto
                {
                    DoctorName = g.Key,
                    AppointmentCount = g.Count()
                })
                .ToListAsync();
        }
    }
}
