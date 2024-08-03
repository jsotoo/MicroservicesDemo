using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Common;

namespace Net.Microservices.CleanArchitecture.Core.Application.Commands
{
    public record ChangeUserPasswordCommand(string Username, string Password) : IRequest<Result>
    {
    }

    /// <summary>
    /// Create User Command Handler
    /// </summary>
    public class ChangeUserPasswordCommandHandler : BaseMessageHandler<ChangeUserPasswordCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public ChangeUserPasswordCommandHandler(IApplicationUserService applicationUserService) {
            _applicationUserService = applicationUserService;
        }

        public override async Task<Result> HandleAsync(ChangeUserPasswordCommand command) {
            return await _applicationUserService.ChangePassword(command.Username, command.Password);
        }
    }
}