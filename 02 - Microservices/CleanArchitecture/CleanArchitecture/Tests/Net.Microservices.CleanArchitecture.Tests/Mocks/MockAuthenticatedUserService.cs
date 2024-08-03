using Net.Microservices.CleanArchitecture.Core.Application;
using Net.Microservices.CleanArchitecture.Core.Domain;

namespace Net.Microservices.CleanArchitecture.Tests.Mocks
{
    internal class MockAuthenticatedUserService : IAuthenticatedUserService
    {
        public string UserId => "TestingUser";

        public string Username => "Mr.Tester";

        public string Name => "Tester Tester";

        public IEnumerable<RolesEnum> Roles { get; set; } = new List<RolesEnum>();
    }
}
