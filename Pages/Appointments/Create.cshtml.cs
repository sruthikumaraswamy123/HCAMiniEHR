using HCAMiniEHR.Data.DbContext;
using HCAMiniEHR.Models;
using HCAMiniEHR.Services;
using HCAMiniEHR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HCAMiniEHR.Pages.Appointments
{
    public class CreateModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ApplicationDbContext _context;

        public CreateModel(IAppointmentService appointmentService, ApplicationDbContext context)
        {
            _appointmentService = appointmentService;
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = new();

        public SelectList PatientList { get; set; } = null!;
        public SelectList DoctorList { get; set; } = null!;

        public void OnGet()
        {
            PatientList = new SelectList(_context.Patients, "PatientId", "FullName");
            DoctorList = new SelectList(_context.Doctors, "DoctorId", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _appointmentService.AddAppointmentAsync(Appointment);
            return RedirectToPage("Index");
        }
    }
}
