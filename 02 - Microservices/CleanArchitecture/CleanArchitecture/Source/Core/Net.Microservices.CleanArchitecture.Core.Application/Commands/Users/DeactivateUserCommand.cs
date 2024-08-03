﻿using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Common;

namespace Net.Microservices.CleanArchitecture.Core.Application.Commands
{
    public record DeactivateUserCommand(string Username) : IRequest<Result>
    {
    }

    /// <summary>
    /// Deactivate User Command Handler
    /// </summary>
    public class DeactivateUserCommandHandler : BaseMessageHandler<DeactivateUserCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public DeactivateUserCommandHandler(IApplicationUserService applicationUserService) {
            _applicationUserService = applicationUserService;
        }

        public override async Task<Result> HandleAsync(DeactivateUserCommand command) {
            return await _applicationUserService.DeactivateUser(command.Username);
        }
    }
}