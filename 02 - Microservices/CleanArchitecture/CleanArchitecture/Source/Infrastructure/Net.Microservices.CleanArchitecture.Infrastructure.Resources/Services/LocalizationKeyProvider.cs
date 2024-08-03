using Net.Microservices.CleanArchitecture.Core.Application;
using Net.Microservices.CleanArchitecture.Core.Domain;

namespace Net.Microservices.CleanArchitecture.Infrastructure.Resources
{
    public class LocalizationKeyProvider : ILocalizationKeyProvider
    {
        public string GetRoleLocalizationKey(RolesEnum role) {
            return role switch
            {
                RolesEnum.User => ResourceKeys.Roles_User,
                RolesEnum.Admin => ResourceKeys.Roles_Admin,
                RolesEnum.SuperAdmin => ResourceKeys.Roles_SuperAdmin,
                _ => role.ToString()
            };
        }
    }
}
