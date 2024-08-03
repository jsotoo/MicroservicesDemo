using System.Linq;
using AutoMapper;
using Net.Microservices.CleanArchitecture.Core.Application.Commands;
using Net.Microservices.CleanArchitecture.Core.Application.DTOs;
using Net.Microservices.CleanArchitecture.Core.Application.Mappings;
using Net.Microservices.CleanArchitecture.Core.Application.ReadModels;
using Net.Microservices.CleanArchitecture.Core.Domain;
using Net.Microservices.CleanArchitecture.Core.Domain.Entities.UserAggregate;
using Net.Microservices.CleanArchitecture.Presentation.Web.ViewModels;

namespace Net.Microservices.CleanArchitecture.Presentation.Web.Mappings
{
    public class AppProfile : Profile
    {
        public AppProfile() {
            #region Commands

            CreateMap<CreateUserViewModel, CreateUserCommand>()
                .ForMember(target => target.Roles, opt => opt.MapFrom(m => m.Roles.Select(r => r.ToString())));
            CreateMap<EditUserViewModel, UpdateUserDetailsCommand>();
            CreateMap<EditUserViewModel, UpdateUserRolesCommand>()
                 .ForMember(target => target.Roles, opt => opt.MapFrom(m => m.Roles.Select(r => r.ToString())));

            #endregion

            #region User

            CreateMap<LoginViewModel, LoginRequestDTO>();
            CreateMap<UserReadModel, EditUserViewModel>();
            CreateMap<UserReadModel, UserDetailsModel>();
            CreateMap<User, UserReadModel>()
               .ForMember(target => target.LocalizedRoles, source => source.MapFrom<LocalizedRolesResolver>());

            #endregion

            CreateMap<string, PhoneNumber>().ConvertUsing<StringToPhoneNumberConverter>();
            CreateMap<PhoneNumber, string>().ConvertUsing<PhoneNumberToStringConverter>();
        }
    }
}
