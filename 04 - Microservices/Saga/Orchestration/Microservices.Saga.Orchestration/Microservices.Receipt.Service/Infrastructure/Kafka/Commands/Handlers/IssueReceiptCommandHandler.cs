using MediatR;
using Microservices.Messages.Service.Orchestrator;
using Microservices.Messages.Service.Receipt.Commands;
using Microservices.Receipt.Service.Application.Interfaces;

namespace Microservices.Receipt.Service.Infrastructure.Kafka.Commands.Handlers
{
    public class IssueReceiptCommandHandler : IRequestHandler<IssueReceiptCommand>
    {
        private readonly IIssueReceiptUseCase _issueReceiptUseCase;

        public IssueReceiptCommandHandler(IIssueReceiptUseCase issueReceiptUseCase)
        {
            _issueReceiptUseCase = issueReceiptUseCase;
        }
        public async Task Handle(IssueReceiptCommand request, CancellationToken cancellationToken)
        {
            await _issueReceiptUseCase.ExecuteAsync(request.TransactionId,request.AccountId,request.Amount,request.Type);
        }
    }
}
