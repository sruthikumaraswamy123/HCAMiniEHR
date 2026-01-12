using System.Collections.Generic;
using System.Threading.Tasks;
using HCAMiniEHR.Models;
using HCAMiniEHR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HCAMiniEHR.Pages.Patients
{
    public partial class IndexModel : PageModel
    {
        private readonly IPatientService _patientService;

        public IndexModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public IList<Patient> Patients { get; set; } = new List<Patient>();

        public async Task OnGetAsync()
        {
            Patients = await _patientService.GetAllPatientsAsync();
        }
    }
}