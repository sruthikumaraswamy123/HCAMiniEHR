using HCAMiniEHR.Data.DbContext;
using HCAMiniEHR.Models;
using HCAMiniEHR.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HCAMiniEHR.Repositories.Implementations
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task AddAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string fullName, string phone, int? excludeId = null)
        {
            var query = _context.Patients.AsQueryable();
            if (excludeId.HasValue)
            {
                query = query.Where(p => p.PatientId != excludeId.Value);
            }
            return await query.AnyAsync(p => p.FullName == fullName && p.Phone == phone);
        }
    }
}
