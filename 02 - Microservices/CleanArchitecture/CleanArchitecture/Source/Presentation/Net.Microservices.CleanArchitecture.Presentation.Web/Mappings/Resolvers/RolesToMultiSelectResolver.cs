using System.Collections.Generic;
using AutoMapper;
using Net.Microservices.CleanArchitecture.Core.Application.ReadModels;
using Net.Microservices.CleanArchitecture.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Net.Microservices.CleanArchitecture.Presentation.Web.Mappings
{
    public class RolesToMultiSelectResolver<T> : IValueResolver<UserReadModel, T, IEnumerable<SelectListItem>>
    {
        private readonly IHtmlHelper _htmlHelper;
        public RolesToMultiSelectResolver(IHtmlHelper htmlHelper) {
            _htmlHelper = htmlHelper;
        }

        public IEnumerable<SelectListItem> Resolve(UserReadModel source, T destination, IEnumerable<SelectListItem> destinationMember, ResolutionContext context) {
            var result = _htmlHelper.GetEnumSelectList<RolesEnum>();
            //foreach (SelectListItem role in result)
            //{
            //    if (source.Roles.Contains(role.Text))
            //        role.Selected = true;
            //}
            return result;
        }
    }
}
