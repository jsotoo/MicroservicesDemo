using Microservices.Demo.Policy.API.CQRS.Queries.Infrastructure.Dtos.Policy;
using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;
using RestEase;

namespace Microservices.Demo.Report.API.Infrastructure.Agents.Policy
{
    public interface IPolicyAgent
    {
        Task<IEnumerable<PolicyDetailsDto>> GetAllPolicies();
    }
}
