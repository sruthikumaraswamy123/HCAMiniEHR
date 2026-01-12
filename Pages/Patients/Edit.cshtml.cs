using HCAMiniEHR.Models;
using HCAMiniEHR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HCAMiniEHR.Pages.Patients
{
    public class EditModel : PageModel
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<EditModel> _logger;

        public EditModel(IPatientService patientService, ILogger<EditModel> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [BindProperty]
        public Patient Patient { get; set; } = new Patient();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Patient = await _patientService.GetPatientByIdAsync(id) ?? new Patient();
            if (Patient.PatientId == 0)
            {
                // not found
                return RedirectToPage("Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                await _patientService.UpdatePatientAsync(Patient);
                _logger.LogInformation("Updated patient {Id} {Name}", Patient.PatientId, Patient.FullName);
                return RedirectToPage("Index");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Update failed - patient not found: {Id}", Patient.PatientId);
                ModelState.AddModelError(string.Empty, "Patient not found.");
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating patient {Id}", Patient.PatientId);
                ModelState.AddModelError(string.Empty, "An unexpected error occurred while updating the patient.");
                return Page();
            }
        }
    }
}