using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;
using RestEase;

namespace Microservices.Demo.Report.API.Infrastructure.Agents.Product
{
    public interface IProductClient
    {
        [Get("/api/products")]
        Task<IEnumerable<ProductDto>> GetAll();
    }
}
