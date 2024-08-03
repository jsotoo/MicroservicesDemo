﻿using System.Collections.Generic;
using AutoMapper;
using Net.Microservices.CleanArchitecture.Core.Application;
using Net.Microservices.CleanArchitecture.Core.Application.ReadModels;
using Net.Microservices.CleanArchitecture.Core.Domain.Entities.UserAggregate;

namespace Net.Microservices.CleanArchitecture.Presentation.Web.Mappings
{
    public class LocalizedRolesResolver : IValueResolver<User, UserReadModel, IReadOnlyCollection<string>>
    {
        private ILocalizationService _localizationService;
        private readonly ILocalizationKeyProvider _localizationKeyProvider;

        public LocalizedRolesResolver(ILocalizationService localizationService, ILocalizationKeyProvider localizationKeyProvider) {
            _localizationService = localizationService;
            _localizationKeyProvider = localizationKeyProvider;
        }

        public IReadOnlyCollection<string> Resolve(User source, UserReadModel destination, IReadOnlyCollection<string> destMember, ResolutionContext context) {
            var result = new List<string>();
            foreach (var role in source.Roles) {
                result.Add(_localizationService[_localizationKeyProvider.GetRoleLocalizationKey(role)]);
            }
            return result;
        }
    }
}
