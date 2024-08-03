using Net.Microservices.CleanArchitecture.Core.Domain;

namespace Net.Microservices.CleanArchitecture.Core.Application
{
    /// <summary>
    /// Provides various localization keys for resolving to localized resources
    /// </summary>
    public interface ILocalizationKeyProvider
    {
        string GetRoleLocalizationKey(RolesEnum role);
    }
}
