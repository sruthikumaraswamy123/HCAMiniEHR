using HCAMiniEHR.Models;
using HCAMiniEHR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HCAMiniEHR.Pages.Appointments
{
    public class DeleteModel : PageModel
    {
        private readonly IAppointmentService _service;

        public DeleteModel(IAppointmentService service)
        {
            _service = service;
        }

        [BindProperty]
        public Appointment? Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Appointment = await _service.GetByIdAsync(id);

            if (Appointment == null)
            {
                TempData["ErrorMessage"] = "Appointment not found.";
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "Appointment deleted successfully.";
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                Appointment = await _service.GetByIdAsync(id);
                ModelState.AddModelError(string.Empty, $"Error deleting appointment: {ex.Message}");
                return Page();
            }
        }
    }
}
