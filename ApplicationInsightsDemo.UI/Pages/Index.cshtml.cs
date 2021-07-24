using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationInsightsDemo.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ApplicationInsightsDemo.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Product> Products;

        private readonly IProductService _productSerivce;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IProductService productService, ILogger<IndexModel> logger)
        {
            _productSerivce = productService;
            _logger = logger;
        }

        public async Task OnGet()
        {
            _logger.LogInformation("Retrieving product information");
            Products = await _productSerivce.GetProducts();
            _logger.LogInformation("Successfully retrieved product information");
        }
    }
}
