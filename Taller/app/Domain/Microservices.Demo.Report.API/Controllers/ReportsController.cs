using Microservices.Demo.Report.API.Application;
using Microservices.Demo.Report.API.CQRS.Queries.GetReportQueries;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Demo.Report.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportApplicationService _reportApplicationService;

        public ReportsController(ReportApplicationService reportApplicationService)
        {
            _reportApplicationService = reportApplicationService;
        }

        [HttpGet("policies")]
        public async Task<ActionResult> GetPoliciesReport([FromQuery] GetReportQueriesQuery query, [FromHeader] string agentLogin)
        {
            return new JsonResult(await _reportApplicationService.GetAllAsync(query, agentLogin));
        }
    }
}
