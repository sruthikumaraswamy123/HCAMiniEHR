using HCAMiniEHR.Models;
using HCAMiniEHR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HCAMiniEHR.Pages.Patients
{
    public class DeleteModel : PageModel
    {
        private readonly IPatientService _patientService;

        public DeleteModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [BindProperty]
        public Patient Patient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Patient = (await _patientService.GetPatientByIdAsync(id))!;
            if (Patient == null)
                return RedirectToPage("Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _patientService.DeletePatientAsync(id);
            return RedirectToPage("Index");
        }
    }
}
