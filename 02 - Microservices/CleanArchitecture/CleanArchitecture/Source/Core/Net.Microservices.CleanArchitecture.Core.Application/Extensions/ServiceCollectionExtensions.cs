using Net.Microservices.CleanArchitecture.Core.Application.Authorization;
using Net.Microservices.CleanArchitecture.Core.Application.Commands;
using Net.Microservices.CleanArchitecture.Core.Application.Mappings;
using Net.Microservices.CleanArchitecture.Core.Application.Queries;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Net.Microservices.CleanArchitecture.Core.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services) {
            services.AddScoped<PhoneNumberToStringConverter>();
            services.AddScoped<StringToPhoneNumberConverter>();

            services.AddAutoMapper(config =>
            {
                config.ConstructServicesUsing(t => services.BuildServiceProvider().GetRequiredService(t));
                config.AddProfile<UserProfile>();
                config.AddProfile<OrderProfile>();
            });
            services.AddMediator(x =>
            {
                #region Commands

                #region User

                x.AddConsumer<CreateUserCommandHandler>();
                x.AddConsumer<ActivateUserCommandHandler>();
                x.AddConsumer<DeactivateUserCommandHandler>();
                x.AddConsumer<AddRolesCommandHandler>();
                x.AddConsumer<RemoveRolesCommandHandler>();
                x.AddConsumer<ChangeUserPasswordCommandHandler>();
                x.AddConsumer<UpdateUserRolesCommandHandler>();
                x.AddConsumer<UpdateUserDetailsCommandHandler>();
                x.AddConsumer<CreateNewOrderCommandHandler>();
                x.AddConsumer<AddOrderItemCommandHandler>();

                #endregion User

                #endregion Commands

                #region Queries

                x.AddConsumer<GetAllUsersQueryHandler>();
                x.AddConsumer<GetUserQueryHandler>();

                #endregion
            });

            services.AddAuthorizationCore();
            services.AddAuthorizationPolicies();
        }

        private static void AddAuthorizationPolicies(this IServiceCollection services) {
            services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, UserOperationAuthorizationHandler>();
        }
    }
}