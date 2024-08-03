using System.Collections.Generic;
using Net.Microservices.CleanArchitecture.Core.Application.Commands;
using Net.Microservices.CleanArchitecture.Core.Domain;
using Net.Microservices.CleanArchitecture.Tests;
using FluentAssertions;
using Xunit;

namespace Net.Microservices.CleanArchitecture.Core.Tests.Application.Commands
{
    public class AddRolesTests : TestBase
    {
        public AddRolesTests() {

        }
        [Fact]
        public async void AddRoles_WhenRoleIsValidAndNotAssigned_ShouldSucceed() {
            var command = new AddRolesCommand("basicUser", new List<string>
                {
                    RolesEnum.SuperAdmin.ToString()
                });

            var result = await ServiceBus.Send(command);

            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async void AddRoles_WhenRoleIsInvalid_ShouldFail() {
            var command = new AddRolesCommand("basicUser", new List<string>
                {
                    "InvalidRole"
                });

            var result = await ServiceBus.Send(command);

            result.Succeeded.Should().BeFalse();
            result.Errors.Should().Contain(e => e.Error == $"Role InvalidRole is invalid.");
        }
    }
}
