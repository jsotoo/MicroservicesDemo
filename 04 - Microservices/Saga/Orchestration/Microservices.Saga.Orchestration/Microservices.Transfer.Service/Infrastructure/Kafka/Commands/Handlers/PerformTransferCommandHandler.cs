using MediatR;
using Microservices.Messages.Service.Orchestrator;
using Microservices.Messages.Service.Transfer.Commands;
using Microservices.Messages.Service.Validator.Commands;
using Microservices.Transfer.Service.Application.Interfaces;

namespace Microservices.Transfer.Service.Infrastructure.Kafka.Commands.Handlers
{
    public class PerformTransferCommandHandler : IRequestHandler<PerformTransferCommand>
    {
        private readonly IPerformTransferUseCase _performTransferUseCase;

        public PerformTransferCommandHandler(IPerformTransferUseCase performTransferUseCase)
        {
            _performTransferUseCase = performTransferUseCase;
        }
        public async Task Handle(PerformTransferCommand request, CancellationToken cancellationToken)
        {
            await _performTransferUseCase.ExecuteAsync(request.TransactionId,request.AccountId,request.Amount,request.Type);
        }
    }
}
