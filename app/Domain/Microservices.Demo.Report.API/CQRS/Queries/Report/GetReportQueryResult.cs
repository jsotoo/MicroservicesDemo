﻿namespace Microservices.Demo.Report.API.CQRS.Queries.Report
{
    public class GetReportQueryResult
    {
        public int Policy { get; set; }
        public string ?FirstNameHolder { get; set; }
        public string ?LastNameHolder { get; set; }
        public string ?DescriptionProduct { get; set; }
    }
}