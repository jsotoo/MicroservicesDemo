namespace Microservices.Saga.Orchestrator.Service.Application.Enums
{
    public enum WorkflowState
    {
        StartSaga,
        SagaStarted,
        AccountValidated,
        Transferred,
        ReceiptIssued,
        Completed
    }
}
