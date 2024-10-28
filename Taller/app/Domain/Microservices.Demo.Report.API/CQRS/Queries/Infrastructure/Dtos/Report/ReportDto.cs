namespace Microservices.Demo.Report.API.CQRS.Queries.Infrastructure.Dtos.Report
{
    public class ReportDto
    {
        public int PolicyID { get; set; }//Policy table
        public string PolicyNumber { get; set; } //Policy table
        public string ProductCode { get; set; } // Product table
        public string ProductName { get; set; } // Product table
        public int PolicyStatusID { get; set; } // policyStatus table
        public string PolicyHolder { get; set; } //Policyholder
        public string AgentLogin { get; set; }

    }
}
