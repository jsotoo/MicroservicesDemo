using MediatR;
using Microservices.Demo.Policy.API.Infrastructure.Data.Repository;
using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Adapters;
using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;
using Microservices.Demo.Product.API.Infrastructure.Data.Repository;
using Microservices.Demo.Report.API.CQRS.Queries.Infrastructure.Dtos.Report;
using Microservices.Demo.Report.API.Infrastructure.Agents.Policy;
using Microservices.Demo.Report.API.Infrastructure.Agents.Product;

namespace Microservices.Demo.Report.API.CQRS.Queries.GetReportQueries
{
    public class GetReportQueriesHandler : IRequestHandler<GetReportQueriesQuery, IEnumerable<ReportDto>>
    {
        private readonly IPolicyAgent _policyAgent;
        private readonly IProductAgent _productAgent;
        public GetReportQueriesHandler(IPolicyAgent policyAgent, IProductAgent productAgent)
        {
            _productAgent = productAgent ?? throw new ArgumentNullException(nameof(productAgent));
            _policyAgent = policyAgent ?? throw new ArgumentNullException(nameof(policyAgent));
        }
        public async Task<IEnumerable<ReportDto>> Handle(GetReportQueriesQuery request, CancellationToken cancellationToken)
        {
            var resultProducts = await _productAgent.GetAllProducts();
            var resultPolicies = await _policyAgent.GetAllPolicies();

            return resultPolicies.Select(policy => new ReportDto
            {
                PolicyNumber = policy.Number,
                ProductCode = policy.ProductCode,
                PolicyHolder = policy.PolicyHolder,
                ProductName = resultProducts.FirstOrDefault(product => product.Code == policy.ProductCode)?.Name ?? string.Empty,
                AgentLogin = request.AgentLogin
                
                
            }).ToList();
        }
    }
}
