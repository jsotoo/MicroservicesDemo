using Microservices.Messages.Base;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Saga.Orchestrator.Service.Application.Enums;
using Microservices.Messages.Enums;

namespace Microservices.Saga.Orchestrator.Service.Domain.Entities
{
    public class Saga : IEntity
    {
        private Guid _transactionIdId;

        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id
        {
            get => _transactionIdId;
            set => _transactionIdId = value;
        }

        [BsonElement("transactionIdId")]
        public Guid TransactionId
        {
            get => _transactionIdId;
            set => _transactionIdId = value;
        }

        [BsonElement("state")]
        public SagaState State { get; private set; }

        [BsonElement("workflowState")]
        public WorkflowState WorkflowState { get; private set; }



        [BsonElement("accountId")]
        public Guid AccountId { get; private set; }

        [BsonElement("amount")]
        public decimal Amount { get; private set; }

        [BsonElement("transferType")]
        public TransferType TransferType { get; private set; }

        public Saga(Guid transactionId, WorkflowState workflowState)
        {
            TransactionId = transactionId;
            WorkflowState = workflowState;            
        }
        public Saga(Guid accountId, decimal amount, TransferType type, WorkflowState workflowState)
        {   
            WorkflowState = workflowState;
            AccountId = accountId;
            Amount = amount;
            TransferType = type;
        }
        public Saga(Guid transactionId, SagaState state, WorkflowState workflowState, Guid accountId, decimal amount, TransferType type)
        {
            TransactionId = transactionId;
            State = state;
            WorkflowState = workflowState;
            AccountId = accountId;
            Amount = amount;
            TransferType = type;
        }
        public void Started()
        {
            State = SagaState.PENDING;
            WorkflowState = WorkflowState.SagaStarted;
        }
        public void AccountValidated()
        {
            State = SagaState.PENDING;
            WorkflowState = WorkflowState.AccountValidated;
        }
        public void Transferred()
        {
            State = SagaState.PENDING;
            WorkflowState = WorkflowState.Transferred;
        }
        public void ReceiptIssued()
        {
            State = SagaState.PENDING;
            WorkflowState = WorkflowState.ReceiptIssued;
        }
        public void Completed()
        {
            State = SagaState.SUCCESS;
            WorkflowState = WorkflowState.Completed;
        }
    }
}
