using System.Threading.Tasks;
using HCAMiniEHR.Models;
using HCAMiniEHR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HCAMiniEHR.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IPatientService patientService, ILogger<CreateModel> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [BindProperty]
        public Patient Patient { get; set; } = new Patient();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState invalid when creating patient: {@ModelState}", ModelState);
                return Page();
            }

            try
            {
                if (await _patientService.IsDuplicateAsync(Patient.FullName, Patient.Phone))
                {
                    ModelState.AddModelError("Patient.FullName", "A patient with this name and phone number already exists.");
                    return Page();
                }

                await _patientService.CreatePatientAsync(Patient);
                _logger.LogInformation("Created patient {FullName} (Phone: {Phone})", Patient.FullName, Patient.Phone);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating patient");
                ModelState.AddModelError(string.Empty, "An unexpected error occurred saving the patient. Check logs for details.");
                return Page();
            }
        }
    }
}