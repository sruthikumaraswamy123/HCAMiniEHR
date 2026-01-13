using HCAMiniEHR.Models;
using HCAMiniEHR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HCAMiniEHR.Pages.Appointments
{
    public class EditModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        public EditModel(IAppointmentService appointmentService, IPatientService patientService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        [BindProperty]
        public Appointment Appointment { get; set; } = null!;

        public SelectList PatientList { get; set; } = null!;
        public SelectList DoctorList { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var appt = await _appointmentService.GetByIdAsync(id);
            if (appt == null)
            {
                TempData["ErrorMessage"] = "Appointment not found.";
                return RedirectToPage("Index");
            }

            Appointment = appt;
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

            try
            {
                await _appointmentService.UpdateAsync(Appointment);
                TempData["SuccessMessage"] = "Appointment updated successfully.";
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                await LoadLists();
                ModelState.AddModelError(string.Empty, $"Error updating appointment: {ex.Message}");
                return Page();
            }
        }

        private async Task LoadLists()
        {
            PatientList = new SelectList(
                await _patientService.GetAllPatientsAsync(),
                "PatientId", "FullName");

            DoctorList = new SelectList(
                await _doctorService.GetAllDoctorsAsync(),
                "DoctorId", "Name");
        }
    }
}
