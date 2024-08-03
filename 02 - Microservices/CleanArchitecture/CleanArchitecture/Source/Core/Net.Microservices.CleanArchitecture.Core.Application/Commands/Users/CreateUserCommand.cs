﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Common;
using Net.Microservices.CleanArchitecture.Core.Domain.Entities.UserAggregate;

namespace Net.Microservices.CleanArchitecture.Core.Application.Commands
{
    public record CreateUserCommand(string Username, string Name, string Email, string Password, List<string> Roles) : IRequest<Result>
    {
    }

    /// <summary>
    /// Create User Command Handler
    /// </summary>
    public class CreateUserCommandHandler : BaseMessageHandler<CreateUserCommand, Result>
    {
        private readonly IApplicationUserService _applicationUserService;

        public CreateUserCommandHandler(IApplicationUserService applicationUserService) {
            _applicationUserService = applicationUserService;
        }

        public async override Task<Result> HandleAsync(CreateUserCommand command) {
            var user = new User(command.Username, command.Email, command.Name);
            return await _applicationUserService.CreateUser(user, command.Password, command.Roles, true);
        }
    }
}