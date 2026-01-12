using HCAMiniEHR.Models;
using HCAMiniEHR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HCAMiniEHR.Pages.LabOrders
{
    public class CreateModel : PageModel
    {
        private readonly ILabOrderService _labService;
        private readonly IAppointmentService _apptService;

        public CreateModel(ILabOrderService labService, IAppointmentService apptService)
        {
            _labService = labService;
            _apptService = apptService;
        }

        [BindProperty]
        public LabOrder LabOrder { get; set; } = new();

        public SelectList AppointmentList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var appts = await _apptService.GetAllAsync();
            // Show Patient Name and Date in Dropdown
            // Needs Appointment to include Patient. ensure GetAllAsync includes them.
            // Repo GetAllAsync includes Patient/Doctor (Step 253).
            
            AppointmentList = new SelectList(appts.Select(a => new {
                Id = a.AppointmentId,
                Text = $"Appt #{a.AppointmentId} - {a.Patient?.FullName} ({a.AppointmentDate.ToShortDateString()})"
            }), "Id", "Text");
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload list on error
                 var appts = await _apptService.GetAllAsync();
                 AppointmentList = new SelectList(appts.Select(a => new {
                    Id = a.AppointmentId,
                    Text = $"Appt #{a.AppointmentId} - {a.Patient?.FullName} ({a.AppointmentDate.ToShortDateString()})"
                }), "Id", "Text");
                return Page();
            }

            LabOrder.OrderDate = DateTime.Now;
            LabOrder.Status = "Pending"; // Default
            await _labService.AddAsync(LabOrder);

            return RedirectToPage("./Index");
        }
    }
}
