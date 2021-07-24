using System.Threading.Tasks;

namespace ApplicationInsightsDemo.UI.Services
{
    public class ExpenseService : IExpenseService
    {
        public async Task<bool> EmployeeIsElegible(string name)
        {
            await Task.Delay(100);

            return name.Length > 10;
        }

        public async Task<bool> IsAmountAcceptable(decimal amount)
        {
            await Task.Delay(2000);

            return amount < 100;
        }

        public async Task<bool> IsDescriptionSufficient(string description)
        {
            await Task.Delay(1000);

            return description.Length > 5;
        }
    }
}
