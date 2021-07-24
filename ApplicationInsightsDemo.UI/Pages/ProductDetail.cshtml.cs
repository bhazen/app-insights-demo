using System.Threading.Tasks;
using ApplicationInsightsDemo.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApplicationInsightsDemo.UI.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductService _productService;

        [BindProperty(SupportsGet = true)]
        public int CategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ProductId { get; set; }

        public Product Product { get; set; }

        public ProductDetailModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task OnGet()
        {
            Product = await _productService.GetProduct(ProductId, CategoryId);
        }
    }
}
