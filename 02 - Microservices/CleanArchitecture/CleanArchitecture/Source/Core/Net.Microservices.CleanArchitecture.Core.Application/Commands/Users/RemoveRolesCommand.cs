using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Common;
using Net.Microservices.CleanArchitecture.Core.Application.DTOs;

namespace Net.Microservices.CleanArchitecture.Core.Application.Commands
{
    public record RemoveRolesCommand(string Username, List<string> Roles) : IRequest<Result>
    {
    }

    /// <summary>
    /// Remove role from user command handler
    /// </summary>
    public class RemoveRolesCommandHandler : BaseMessageHandler<RemoveRolesCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public RemoveRolesCommandHandler(IApplicationUserService applicationUserService) {
            _applicationUserService = applicationUserService;
        }

        public override async Task<Result> HandleAsync(RemoveRolesCommand command) {
            var request = new RoleAssignmentRequestDTO
            {
                Username = command.Username,
                Roles = command.Roles
            };

            return await _applicationUserService.RemoveRoles(request);
        }
    }
}