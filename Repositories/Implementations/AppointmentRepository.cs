using HCAMiniEHR.Data;
using HCAMiniEHR.Data.DbContext;
using HCAMiniEHR.Models;
using HCAMiniEHR.Repositories;
using Microsoft.EntityFrameworkCore;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _context;

    public AppointmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.AppointmentId == id);
    }

    public async Task AddAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var appt = await _context.Appointments.FindAsync(id);
        if (appt != null)
        {
            _context.Appointments.Remove(appt);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> AddViaSpAsync(Appointment appt)
    {
        var pId = new Microsoft.Data.SqlClient.SqlParameter("@PatientId", appt.PatientId);
        var date = new Microsoft.Data.SqlClient.SqlParameter("@AppointmentDate", appt.AppointmentDate);
        var reason = new Microsoft.Data.SqlClient.SqlParameter("@Reason", appt.Reason ?? "");
        var status = new Microsoft.Data.SqlClient.SqlParameter("@Status", appt.Status);
        var dId = new Microsoft.Data.SqlClient.SqlParameter("@DoctorId", appt.DoctorId);

        var sql = "EXEC [Healthcare].[sp_CreateAppointment] @PatientId, @AppointmentDate, @Reason, @Status, @DoctorId";
        
        await _context.Database.ExecuteSqlRawAsync(sql, pId, date, reason, status, dId);
        return 0; // Placeholder ID.
    }

    public async Task<List<Appointment>> GetByPatientIdAsync(int patientId)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.PatientId == patientId)
            .ToListAsync();
    }
}
