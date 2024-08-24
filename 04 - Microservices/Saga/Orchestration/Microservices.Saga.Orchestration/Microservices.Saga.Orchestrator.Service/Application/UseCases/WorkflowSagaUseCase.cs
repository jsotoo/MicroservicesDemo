
using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Enums;
using Microservices.Saga.Orchestrator.Service.Application.Enums;
using Microservices.Saga.Orchestrator.Service.Application.Interfaces;
using Microservices.Saga.Orchestrator.Service.Core.Results;
using Microservices.Saga.Orchestrator.Service.Domain.Interfaces;

namespace Microservices.Saga.Orchestrator.Service.Application.UseCases
{
    public class WorkflowSagaUseCase : IWorkflowSagaUseCase
    {
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<WorkflowSagaUseCase> _logger;
        private readonly IOrchestratorUnitOfWork _unitOfWork;
        private readonly IStartSagaUseCase _startSagaUseCase;
        private readonly IValidateAccountUseCase _validateAccountUseCase;
        private readonly IPerformTransferUseCase _performTransferUseCase;
        private readonly IssueReceiptUseCase _issueReceiptUseCase;
        private readonly ICompleteSagaUseCase _completeSagaUseCase;

        public WorkflowSagaUseCase(
            IMessageProducer messageProducer,
            ILogger<WorkflowSagaUseCase> logger,
            IOrchestratorUnitOfWork unitOfWork,
            IStartSagaUseCase startSagaUseCase,
            IValidateAccountUseCase validateAccountUseCase,
            IPerformTransferUseCase performTransferUseCase,
            IssueReceiptUseCase issueReceiptUseCase,
            ICompleteSagaUseCase completeSagaUseCase
        )
        {
            _messageProducer = messageProducer;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _startSagaUseCase = startSagaUseCase;
            _validateAccountUseCase = validateAccountUseCase;
            _performTransferUseCase = performTransferUseCase;
            _issueReceiptUseCase = issueReceiptUseCase;
            _completeSagaUseCase = completeSagaUseCase;
        }        
        public async Task<WorkflowResult> ExecuteAsync(Domain.Entities.Saga saga)
        {
            try
            {
                var workflowState = saga.WorkflowState;

                if (saga.TransactionId != Guid.Empty)
                {
                    saga = await _unitOfWork.SagaRepository.GetByIdAsync(saga.TransactionId);
                }

                switch (workflowState)
                {
                    case WorkflowState.StartSaga:
                        var transactionId = (await _startSagaUseCase.ExecuteAsync(saga)).TransactionId;
                        saga.TransactionId = transactionId;
                        break;
                    case WorkflowState.SagaStarted:
                        saga.Started();
                        saga = await _unitOfWork.SagaRepository.UpdateAsync(saga);
                        await _unitOfWork.CommitAsync();
                        await _validateAccountUseCase.ExecuteAsync(saga);
                        break;
                    case WorkflowState.AccountValidated:
                        saga.AccountValidated();
                        saga= await _unitOfWork.SagaRepository.UpdateAsync(saga);
                        await _unitOfWork.CommitAsync();
                        await _performTransferUseCase.ExecuteAsync(saga);
                        break;
                    case WorkflowState.Transferred:
                        saga.Transferred();
                        saga = await _unitOfWork.SagaRepository.UpdateAsync(saga);
                        await _unitOfWork.CommitAsync();
                        await _issueReceiptUseCase.ExecuteAsync(saga);
                        break;
                    case WorkflowState.ReceiptIssued:
                        saga.ReceiptIssued();
                        saga = await _unitOfWork.SagaRepository.UpdateAsync(saga);
                        await _unitOfWork.CommitAsync();
                        await _completeSagaUseCase.ExecuteAsync(saga);
                        break;
                    default:
                        break;
                }

                return new WorkflowResult(true, saga.TransactionId);
            }
            catch (Exception e)
            {
                return new WorkflowResult(false, saga.TransactionId, e.Message);
            }

        }

    }
}
