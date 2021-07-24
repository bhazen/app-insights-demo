using System.Threading.Tasks;

namespace ApplicationInsightsDemo.UI.Services
{
    public interface IExpenseService
    {
        Task<bool> EmployeeIsElegible(string name);
        Task<bool> IsAmountAcceptable(decimal amount);
        Task<bool> IsDescriptionSufficient(string description);
    }
}