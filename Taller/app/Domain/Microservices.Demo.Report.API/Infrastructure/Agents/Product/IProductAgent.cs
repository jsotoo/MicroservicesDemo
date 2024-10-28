using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;

namespace Microservices.Demo.Report.API.Infrastructure.Agents.Product
{
    public interface IProductAgent
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
    }
}
