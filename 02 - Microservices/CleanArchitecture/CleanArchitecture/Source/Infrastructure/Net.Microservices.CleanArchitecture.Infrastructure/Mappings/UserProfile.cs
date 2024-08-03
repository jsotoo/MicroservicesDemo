using AutoMapper;
using Net.Microservices.CleanArchitecture.Common;
using Net.Microservices.CleanArchitecture.Core.Application.ReadModels;
using Net.Microservices.CleanArchitecture.Core.Domain;
using Net.Microservices.CleanArchitecture.Core.Domain.Entities.UserAggregate;
using Net.Microservices.CleanArchitecture.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Net.Microservices.CleanArchitecture.Infrastructure.Mappings
{
    internal class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<ApplicationUserRole, RolesEnum>()
                .ConvertUsing(r => r.Role.Name.ToEnum<RolesEnum>());

            CreateMap<User, ApplicationUser>()
                .ForMember(target => target.Roles, opt => opt.Ignore())
                .ForPath(target => target.PhoneNumber, source => source.MapFrom(m => m.PrimaryPhoneNumber));

            CreateMap<ApplicationUser, User>()
                .ForMember(target => target.PrimaryPhoneNumber, source => source.MapFrom(m => m.PhoneNumber));

            CreateMap<ApplicationUser, UserReadModel>();

            CreateMap<IdentityError, ResultError>()
                .ForMember(target => target.Error, opt => opt.MapFrom(source => source.Description));
        }
    }
}
