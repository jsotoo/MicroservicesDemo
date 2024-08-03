using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Net.Microservices.CleanArchitecture.Common;
using Net.Microservices.CleanArchitecture.Core.Application;
using Net.Microservices.CleanArchitecture.Core.Domain;
using Net.Microservices.CleanArchitecture.Infrastructure.Models;
using Microsoft.AspNetCore.Http;

namespace Net.Microservices.CleanArchitecture.Presentation.Web.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor) {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;
            Username = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? null;
            Name = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUser.FullNameClaimType)?.Value ?? null;
            Culture = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUser.CultureClaimType)?.Value ?? null;
            UiCulture = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUser.UiCultureClaimType)?.Value ?? null;
            Theme = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUser.ThemeClaimType)?.Value.ToEnum<ThemeEnum>() ?? default;
            var profilePictureClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ApplicationUser.ProfilePictureClaimType)?.Value;
            if (profilePictureClaim != null) {
                ProfilePicture = profilePictureClaim;
            }

            var roles = httpContextAccessor.HttpContext?.User?.Claims.Where(c => c.Type == ClaimTypes.Role);
            if (roles != null)
                Roles = roles.Select(r => r.Value.ToEnum<RolesEnum>());
        }

        public string UserId { get; }

        public string Username { get; }
        public string Name { get; }
        public string ProfilePicture { get; }
        public string Culture { get; set; }
        public string UiCulture { get; set; }
        public IEnumerable<RolesEnum> Roles { get; }
        public ThemeEnum Theme { get; private set; }
    }
}