﻿using Microsoft.Extensions.Localization;

namespace Net.Microservices.CleanArchitecture.Core.Application
{
    public interface ILocalizationService
    {
        LocalizedString this[string key] { get; }

        LocalizedString GetLocalizedString(string key);
        LocalizedString GetCulturedLocalizedString(string key, string culture);

        string GetLocalizedString(string key, params string[] parameters);
    }
}
