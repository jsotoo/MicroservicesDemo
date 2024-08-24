using MediatR;
using Microservices.Messages.Service.Orchestrator;
using Microservices.Messages.Service.Validator.Commands;
using Microservices.Validator.Service.Application.Interfaces;

namespace Microservices.Validator.Service.Infrastructure.Kafka.Commands.Handlers
{
    public class ValidateAccountCommandHandler : IRequestHandler<ValidateAccountCommand>
    {
        private readonly IValidateAccountUseCase _validateAccountUseCase;

        public ValidateAccountCommandHandler(IValidateAccountUseCase validateAccountUseCase)
        {
            _validateAccountUseCase = validateAccountUseCase;
        }
        public async Task Handle(ValidateAccountCommand request, CancellationToken cancellationToken)
        {
            await _validateAccountUseCase.ExecuteAsync(request.TransactionId, request.AccountId, request.Amount, request.Type);
        }
    }
}
