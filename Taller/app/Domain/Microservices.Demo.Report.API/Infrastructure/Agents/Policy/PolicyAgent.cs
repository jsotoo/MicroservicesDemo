using Microservices.Demo.Policy.API.CQRS.Queries.Infrastructure.Dtos.Policy;
using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;

namespace Microservices.Demo.Report.API.Infrastructure.Agents.Policy
{
    public class PolicyAgent : IPolicyAgent
    {
        private readonly IPolicyClient _policyClient;
        private readonly ILogger<PolicyAgent> _logger;

        public PolicyAgent(
            IPolicyClient policyClient,
            ILogger<PolicyAgent> logger)
        {
            _policyClient = policyClient;
            _logger = logger;
        }

        public async Task<IEnumerable<PolicyDetailsDto>> GetAllPolicies()
        {
            try
            {
                var products = await _policyClient.GetAll();
                return products.Select(p => new PolicyDetailsDto
                {
                    Number = p.Number,
                    PolicyHolder = p.PolicyHolder,
                    AccountNumber = p.AccountNumber,
                    ProductCode = p.ProductCode
                    
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
