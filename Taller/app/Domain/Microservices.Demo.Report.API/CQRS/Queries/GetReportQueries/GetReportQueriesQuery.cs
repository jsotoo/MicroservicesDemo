using MediatR;
using Microservices.Demo.Report.API.CQRS.Queries.Infrastructure.Dtos.Report;

namespace Microservices.Demo.Report.API.CQRS.Queries.GetReportQueries
{
    public class GetReportQueriesQuery : IRequest<IEnumerable<ReportDto>>
    {
        public string AgentLogin { get; set; }
    }
}
