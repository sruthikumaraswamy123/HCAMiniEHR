using HCAMiniEHR.Data.DbContext;
using HCAMiniEHR.Models;
using HCAMiniEHR.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCAMiniEHR.Repositories.Implementations
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context) => _context = context;

        public async Task<List<Doctor>> GetAllDoctorsAsync()
            => await _context.Doctors.Include(d => d.Appointments).ToListAsync();

        public async Task<Doctor> GetDoctorByIdAsync(int id)
            => await _context.Doctors.Include(d => d.Appointments).FirstOrDefaultAsync(d => d.DoctorId == id);

        public async Task AddDoctorAsync(Doctor doctor) { _context.Doctors.Add(doctor); await _context.SaveChangesAsync(); }
        public async Task UpdateDoctorAsync(Doctor doctor) { _context.Doctors.Update(doctor); await _context.SaveChangesAsync(); }
        public async Task DeleteDoctorAsync(int id) { var d = await _context.Doctors.FindAsync(id); if (d != null) { _context.Doctors.Remove(d); await _context.SaveChangesAsync(); } }
    }
}
