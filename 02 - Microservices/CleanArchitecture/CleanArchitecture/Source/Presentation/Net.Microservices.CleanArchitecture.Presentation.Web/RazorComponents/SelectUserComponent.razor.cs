﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Core.Application.Queries;
using Net.Microservices.CleanArchitecture.Core.Application.ReadModels;
using Net.Microservices.CleanArchitecture.Presentation.Framework.Components;
using Microsoft.AspNetCore.Components;

namespace Net.Microservices.CleanArchitecture.Presentation.Web.RazorComponents
{
    public partial class SelectUserComponent : BaseComponent
    {
        [Parameter]
        public Func<UserReadModel, bool> Filter { get; set; }
        [Parameter]
        public bool ExcludeLoggedInUser { get; set; }
        [Parameter]
        public EventCallback<UserReadModel> OnUserSelected { get; set; }
        [Parameter]
        public bool ShowLoader { get; set; }
        public UserReadModel SelectedUser { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string ParentId { get; set; }

        public IEnumerable<UserReadModel> AvailableUsers { get; set; } = new List<UserReadModel>();

        public Select2<UserReadModel> SelectReference;
        protected override async Task OnParametersSetAsync() {
            var query = new GetAllUsersQuery();

            var result = await SendRequestAsync(query, ShowLoader);

            if (result.Failed) {
                return;
            }

            AvailableUsers = result.Data.ToList();

            var currentUser = await GetCurrentUser();

            if (ExcludeLoggedInUser)
                AvailableUsers = AvailableUsers.Where(u => u.Username != currentUser.Identity.Name);

            if (Filter != null)
                AvailableUsers = AvailableUsers.Where(Filter);

            await base.OnParametersSetAsync();
        }

        public async Task UserSelected(UserReadModel user) {
            SelectReference?.ResetSelection();
            await OnUserSelected.InvokeAsync(user);
        }
    }
}
