using Microservices.Demo.Policy.API.CQRS.Queries.Infrastructure.Dtos.Policy;
using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;
using Microservices.Demo.Report.API.Infrastructure.Agents.Product;
using Microservices.Demo.Report.API.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Polly;
using RestEase;
using Steeltoe.Common.Discovery;
using Steeltoe.Discovery;
using System.Net.Http.Headers;

namespace Microservices.Demo.Report.API.Infrastructure.Agents.Policy
{
    public class PolicyClient : IPolicyClient
    {
        private readonly IPolicyClient _client;
        private readonly ServicesUrl _servicesUrl;

        private static IAsyncPolicy<IEnumerable<PolicyDetailsDto>> _retryPolicy =
          Policy<IEnumerable<PolicyDetailsDto>>
              .Handle<HttpRequestException>()
              .Or<TimeoutException>()
              .WaitAndRetryAsync(3, retryAttempt =>
                  TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        private static IAsyncPolicy<IEnumerable<PolicyDetailsDto>> _circuitBreakerPolicy =
      Policy<IEnumerable<PolicyDetailsDto>>
          .Handle<HttpRequestException>()
          .AdvancedCircuitBreakerAsync(
              failureThreshold: 0.5,
              samplingDuration: TimeSpan.FromSeconds(10),
              minimumThroughput: 8,
              durationOfBreak: TimeSpan.FromSeconds(30)
          );

        public PolicyClient(IOptions<ServicesUrl> servicesUrl, IDiscoveryClient discoveryClient)
        {
            _servicesUrl = servicesUrl.Value;

            var handler = new DiscoveryHttpClientHandler(discoveryClient);

            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var httpClient = new HttpClient(handler, false)
            {
                BaseAddress = new Uri(_servicesUrl.ProductApiUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _client = RestClient.For<IPolicyClient>(httpClient);
        }

        public async Task<IEnumerable<PolicyDetailsDto>> GetAll()
        {
            try
            {
                return await _retryPolicy
                    .WrapAsync(_circuitBreakerPolicy)
                    .ExecuteAsync(async () =>
                    {
                        return await _client.GetAll();
                    });
            }
            catch (HttpRequestException ex)
            {
                // Log del error
                throw new ServiceException("Error getting products", ex);
            }
        }


    }
}
