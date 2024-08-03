using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Common;
using Net.Microservices.CleanArchitecture.Core.Application.DTOs;

namespace Net.Microservices.CleanArchitecture.Core.Application.Commands
{
    public record AddRolesCommand(string Username, List<string> Roles) : IRequest<Result>
    {
    }

    /// <summary>
    /// Add roles to user command handler
    /// </summary>
    public class AddRolesCommandHandler : BaseMessageHandler<AddRolesCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public AddRolesCommandHandler(IApplicationUserService applicationUserService) {
            _applicationUserService = applicationUserService;
        }

        public override async Task<Result> HandleAsync(AddRolesCommand command) {
            var request = new RoleAssignmentRequestDTO
            {
                Username = command.Username,
                Roles = command.Roles
            };

            return await _applicationUserService.AddRoles(request);
        }
    }
}