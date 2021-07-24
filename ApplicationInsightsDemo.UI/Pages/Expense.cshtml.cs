using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ApplicationInsightsDemo.UI.Extensions;
using ApplicationInsightsDemo.UI.Services;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApplicationInsightsDemo.UI.Pages
{
    public class ExpenseModel : PageModel
    {
        private readonly IExpenseService _expenseService;
        private readonly TelemetryClient _telemetryClient;

        [BindProperty]
        [Required]
        public string EmployeeName { get; set; }
        [BindProperty]
        [Required]
        public decimal Amount { get; set; }
        [BindProperty]
        [Required]
        public string Description { get; set; }

        public ExpenseModel(IExpenseService expenseService, TelemetryClient telemetryClient)
        {
            _expenseService = expenseService;
            _telemetryClient = telemetryClient;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using var overAllTimeMetric = _telemetryClient.StartTimeMetric("ExpenseOverallDuration");

            using (var employeIsElilgbleMetric = _telemetryClient.StartTimeMetric("ExpenseEmployeIsEligibleDuration"))
            {
                var employeeIsEligible = await _expenseService.EmployeeIsElegible(EmployeeName);
            }

            using (var amountIsAcceptableMetric = _telemetryClient.StartTimeMetric("ExpenseAmountIsAcceptableDuration"))
            {
                var amountIsAcceptable = await _expenseService.IsAmountAcceptable(Amount);
            }

            using (var descriptionIsSufficientMetric = _telemetryClient.StartTimeMetric("ExpenseDescriptionIsSufficientDuration"))
            {
                var descriptionIsSufficient = await _expenseService.IsDescriptionSufficient(Description);
            }

            return RedirectToPage("./Index");
        }
    }
}
