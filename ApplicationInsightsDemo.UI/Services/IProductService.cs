using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationInsightsDemo.UI.Services
{
    public interface IProductService
    {
        Task<Product> GetProduct(string productId, int categoryId);
        Task<IEnumerable<Product>> GetProducts();
    }
}