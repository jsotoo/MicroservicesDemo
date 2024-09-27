using MediatR;
using Microservices.Demo.Report.API.CQRS.Queries.Infrastructure.Dtos.Report;
using Microservices.Demo.Report.API.CQRS.Queries.Report;

namespace Microservices.Demo.Report.API.Application
{
    public class ReportApplicationService
    {
        private readonly IMediator _mediator;

        public ReportApplicationService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<GetReportQueryResult> GetReportAsync(int policyNumber, string FirstNameHolder, string LastNameHolder, string DescriptionProduct)
        {
            var query = new GetReportQuery { Policy = policyNumber, FirstNameHolder = FirstNameHolder, LastNameHolder = LastNameHolder, DescriptionProduct = DescriptionProduct };
            var result = await _mediator.Send(query);
            return result;
        }


    }
}
