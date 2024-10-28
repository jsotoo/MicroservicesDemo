using MediatR;
using Microservices.Demo.Policy.API.CQRS.Queries.Policy.GetPolicyDetails;
using Microservices.Demo.Policy.API.Infrastructure.Data.Repository;
using Microservices.Demo.Product.API.CQRS.Queries.FindAllProducts;
using Microservices.Demo.Product.API.Infrastructure.Data.Repository;
using Microservices.Demo.Report.API.CQRS.Queries.GetReportQueries;
using Microservices.Demo.Report.API.CQRS.Queries.Infrastructure.Dtos.Report;

namespace Microservices.Demo.Report.API.Application
{
    public class ReportApplicationService
    {
        private readonly IMediator _mediator;
        private readonly IProductRepository _productRepository;
        private readonly IPolicyRepository _policyRepository;
        public ReportApplicationService(IMediator mediator, IProductRepository productRepository, IPolicyRepository policyRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _policyRepository = policyRepository ?? throw new ArgumentNullException(nameof(policyRepository));
        }
        public async Task<IEnumerable<ReportDto>> GetAllAsync(GetReportQueriesQuery query, string agentLogin)
        {
            query.AgentLogin = agentLogin;
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
