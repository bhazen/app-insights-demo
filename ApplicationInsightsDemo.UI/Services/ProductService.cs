using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationInsightsDemo.UI.Services
{
    public class ProductService : IProductService
    {
        public static List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = "1",
                Name = "Switch",
                Description = "Latest console from Nintentdo"
            },
            new Product
            {
                Id = "2",
                Name = "Playstation 5",
                Description = "Latest console from Sony"
            },
            new Product
            {
                Id = "3",
                Name = "Xbox S Series",
                Description = "One of the latest consoles from Microsoft. Not as powerful as the X series"
            },
            new Product
            {
                Id = "4",
                Name = "Xbox X Series",
                Description = "Latest, most powerful console from Microsoft"
            }
        };

        public Task<IEnumerable<Product>> GetProducts()
        {
            return Task.FromResult(Products.AsEnumerable());
        }

        public Task<Product> GetProduct(string productId, int categoryId)
        {
            if (productId == "3")
            {
                throw new Exception("Need to demo this somehow");
            }

            var product = Products.FirstOrDefault(x => x.Id == productId);

            return Task.FromResult(product);
        }
    }
}
