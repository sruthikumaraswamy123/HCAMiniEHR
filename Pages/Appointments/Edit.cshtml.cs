using HCAMiniEHR.Data;
using HCAMiniEHR.Data.DbContext;
using HCAMiniEHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HCAMiniEHR.Pages.Appointments
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = null!;

        public SelectList PatientList { get; set; } = null!;
        public SelectList DoctorList { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Appointment = await _context.Appointments.FindAsync(id);

            if (Appointment == null)
                return RedirectToPage("Index");

            await LoadLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadLists();
                return Page();
            }

            _context.Attach(Appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        private async Task LoadLists()
        {
            PatientList = new SelectList(
                await _context.Patients.ToListAsync(),
                "PatientId", "FullName");

            DoctorList = new SelectList(
                await _context.Doctors.ToListAsync(),
                "DoctorId", "Name");
        }
    }
}
