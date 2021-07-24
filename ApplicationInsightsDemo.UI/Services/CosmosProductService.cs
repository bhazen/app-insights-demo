using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationInsightsDemo.UI.Configuration;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApplicationInsightsDemo.UI.Services
{
    public class CosmosProductService : IProductService
    {
        private Container _container;
        private ILogger<CosmosProductService> _logger;

        public CosmosProductService(CosmosClient cosmosClient, IOptions<CosmosDbOptions> cosmosOptions, ILogger<CosmosProductService> logger)
        {
            _container = cosmosClient.GetContainer(cosmosOptions.Value.DatabaseName, cosmosOptions.Value.ContainerName);
            _logger = logger;
        }

        public async Task<Product> GetProduct(string productId, int categoryId)
        {
            _logger.LogInformation("Fetching production detail information for product {ProductId} in category {CategoryId}", productId, categoryId);
            if (productId == "3")
            {
                _logger.LogError("This is a demo error");
                throw new ArgumentException("Have to demo this somehow", nameof(productId));
            }

            try
            {
                var response = await _container.ReadItemAsync<Product>(productId, new PartitionKey(categoryId));
                _logger.LogInformation("Sucessfully fetched prodcut data");
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogInformation("Did not find product with product id of {ProductId}", productId);
                return null;
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            _logger.LogInformation("Fetching products from Cosmos db");
            var query = _container.GetItemQueryIterator<Product>();
            var results = new List<Product>();
            while (query.HasMoreResults)
            {
                _logger.LogInformation("Fetching all products has more results, loading next set.");
                var response = await query.ReadNextAsync();
                _logger.LogInformation("Fetched {FetchedRecordsCount} from Cosmos", response.Count);

                results.AddRange(response.ToList());
            }

            _logger.LogInformation("Fetched {Count} records from Cosmos", results.Count);

            return results;
        }
    }
}
