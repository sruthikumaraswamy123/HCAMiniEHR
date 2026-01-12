using HCAMiniEHR.Data.DbContext;
using HCAMiniEHR.Models;
using HCAMiniEHR.Repositories;
using HCAMiniEHR.Repositories.Implementations;
using HCAMiniEHR.Repositories.Interfaces;
using HCAMiniEHR.Services;
using HCAMiniEHR.Services.Implementations;
using HCAMiniEHR.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// Database Configuration - SQL Server LocalDB
// =====================================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));

// =====================================================
// Repository DI
// =====================================================
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<ILabOrderRepository, LabOrderRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

// =====================================================
// Service DI
// =====================================================
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<ILabOrderService, LabOrderService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IReportService, ReportService>();

// =====================================================
// Razor Pages
// =====================================================
builder.Services.AddRazorPages();

var app = builder.Build();

// =====================================================
// Ensure database exists / apply migrations at startup
// and seed Doctors if missing
// =====================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var db = services.GetRequiredService<ApplicationDbContext>();

        // Log configured connection metadata
        try
        {
            var conn = db.Database.GetDbConnection();
            logger.LogInformation("Using DB connection - DataSource: {DataSource}, Database: {Database}", conn.DataSource, conn.Database);
        }
        catch
        {
            logger.LogInformation("Unable to read DbConnection metadata.");
        }

        logger.LogInformation("Applying migrations...");
        db.Database.Migrate();
        logger.LogInformation("Migrations applied.");

        // Seed doctors only if none exist
        if (!db.Doctors.Any())
        {
            logger.LogInformation("No doctors found — seeding sample doctors...");
            db.Doctors.AddRange(
                new Doctor { Name = "Amit Sharma", Specialization = "General" },
                new Doctor { Name = "Neha Verma", Specialization = "Pediatrics" },
                new Doctor { Name = "Rahul Mehta", Specialization = "Cardiology" },
                new Doctor { Name = "Priya Nair", Specialization = "Gynecology" },
                new Doctor { Name = "Suresh Kumar", Specialization = "Orthopedics" },
                new Doctor { Name = "Anita Rao", Specialization = "Dermatology" },
                new Doctor { Name = "Vikram Singh", Specialization = "ENT" },
                new Doctor { Name = "Pooja Patel", Specialization = "Neurology" },
                new Doctor { Name = "Rohit Iyer", Specialization = "Urology" },
                new Doctor { Name = "Kavita Joshi", Specialization = "Ophthalmology" }
            );
            db.SaveChanges();
            logger.LogInformation("Seeded doctors. Count now: {Count}", db.Doctors.Count());
        }
        else
        {
            logger.LogInformation("Doctors already exist (count={Count})", db.Doctors.Count());
        }
    }
    catch (Exception ex)
    {
        logger.LogCritical(ex, "Database startup check failed.");
        throw;
    }
}

// =====================================================
// Middleware
// =====================================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
