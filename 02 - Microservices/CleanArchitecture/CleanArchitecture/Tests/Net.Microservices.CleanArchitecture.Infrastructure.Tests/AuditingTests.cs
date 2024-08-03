using System.Linq;
using Net.Microservices.CleanArchitecture.Infrastructure.Models;
using Net.Microservices.CleanArchitecture.Tests;
using Xunit;

namespace Net.Microservices.CleanArchitecture.Infrastructure.Tests
{
    public class AuditingTests : TestBase
    {
        [Fact]
        public void Auditing_OnAdd_AuditableEntity_CreatesAuditHistoryRecord()
        {
            OrderEntity order = new OrderEntity();
            ApplicationContext.Orders.Add(order);
            ApplicationContext.SaveChanges();

            var auditHistory = ApplicationContext.AuditHistory.SingleOrDefault();

            Assert.NotNull(auditHistory);
        }
    }
}