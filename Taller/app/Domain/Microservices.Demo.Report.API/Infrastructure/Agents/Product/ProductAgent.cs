using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;

namespace Microservices.Demo.Report.API.Infrastructure.Agents.Product
{
    public class ProductAgent : IProductAgent
    {
        private readonly IProductClient _productClient;
        private readonly ILogger<ProductAgent> _logger;

        public ProductAgent(
            IProductClient productClient,
            ILogger<ProductAgent> logger)
        {
            _productClient = productClient;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            try
            {
                var products = await _productClient.GetAll();
                return products.Select(p => new ProductDto
                {
                    
                    Code = p.Code,
                    Name = p.Name,
                    Description = p.Description
                });
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Error getting products from service");
                throw;
            }
        }

    }
}
