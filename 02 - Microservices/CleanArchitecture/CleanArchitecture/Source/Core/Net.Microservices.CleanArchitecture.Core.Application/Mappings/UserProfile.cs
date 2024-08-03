using AutoMapper;
using Net.Microservices.CleanArchitecture.Core.Application.Commands;
using Net.Microservices.CleanArchitecture.Core.Application.DTOs;

namespace Net.Microservices.CleanArchitecture.Core.Application
{
    internal class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<UpdateUserDetailsCommand, UpdateUserDetailsDTO>();
        }
    }
}