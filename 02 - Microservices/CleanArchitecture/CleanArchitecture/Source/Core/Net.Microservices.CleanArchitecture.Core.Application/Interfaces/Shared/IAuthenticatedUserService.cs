using System.Collections.Generic;
using Net.Microservices.CleanArchitecture.Core.Domain;

namespace Net.Microservices.CleanArchitecture.Core.Application
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        public string Username { get; }
        public string Name { get; }
        public IEnumerable<RolesEnum> Roles { get; }
    }
}