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
            await LoadAppointments();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadAppointments();
                return Page();
            }

            Console.Write(LabOrder);

            LabOrder.OrderDate = DateTime.Now;
            LabOrder.Status = "Pending";

            await _labService.AddAsync(LabOrder);

            return RedirectToPage("./Index");
        }

        private async Task LoadAppointments()
        {
            var appts = await _apptService.GetAllAsync();

            AppointmentList = new SelectList(
                appts.Select(a => new
                {
                    Id = a.AppointmentId,
                    Text = $"Appt #{a.AppointmentId} - {a.Patient?.FullName} ({a.AppointmentDate:dd-MMM-yyyy})"
                }),
                "Id",
                "Text"
            );
        }
    }
}
