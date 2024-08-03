using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Common;
using Net.Microservices.CleanArchitecture.Core.Application.DTOs;

namespace Net.Microservices.CleanArchitecture.Core.Application
{
    public interface IUserAuthenticationService
    {
        Task<Result<string>> Login(LoginRequestDTO loginRequest);
        Task<Result> Logout();
    }
}
