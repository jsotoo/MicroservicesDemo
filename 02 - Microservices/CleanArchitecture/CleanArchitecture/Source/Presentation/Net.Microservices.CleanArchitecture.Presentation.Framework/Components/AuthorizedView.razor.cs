﻿using System;
using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Presentation.Framework.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Net.Microservices.CleanArchitecture.Presentation.Framework.Components
{
    public partial class AuthorizedView : BaseComponent
    {
        [Inject]
        public IAuthorizationStateProvider AuthorizationState { get; set; }

        [Parameter]
        public Func<bool> Expression { get; set; }

        [Parameter]
        public IAuthorizationRequirement Operation { get; set; }

        [Parameter]
        public RenderFragment Success { get; set; }

        [Parameter]
        public RenderFragment Failed { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public bool Visible { get; set; }

        protected override async Task OnParametersSetAsync() {
            Visible = await AuthorizationState.TryAddAndCheckRequirement(Operation);

            await base.OnParametersSetAsync();
        }
    }
}
