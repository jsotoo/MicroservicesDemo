using System.Threading;
using System.Threading.Tasks;

namespace Net.Microservices.CleanArchitecture.Core.Application
{
    public interface IServiceBus
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default) where TResponse : class;
    }
}