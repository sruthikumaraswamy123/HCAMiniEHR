using HCAMiniEHR.Models;
using HCAMiniEHR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HCAMiniEHR.Pages.Reports
{
    public class IndexModel : PageModel
    {
        private readonly IReportService _reportService;

        public IndexModel(IReportService reportService)
        {
            _reportService = reportService;
        }

        public List<LabOrder> PendingLabOrders { get; set; } = new();
        public List<Patient> PatientsNoFollowUp { get; set; } = new();
        public List<DoctorProductivityDto> DoctorProductivity { get; set; } = new();

        // Add this property to get the pending count
        public int PendingLabOrdersCount => PendingLabOrders.Count;

        public async Task OnGetAsync()
        {
            PendingLabOrders = await _reportService.GetPendingLabOrdersAsync();
            PatientsNoFollowUp = await _reportService.GetPatientsWithoutFollowUpAsync();
            DoctorProductivity = await _reportService.GetDoctorProductivityAsync();
        }
    }
}
