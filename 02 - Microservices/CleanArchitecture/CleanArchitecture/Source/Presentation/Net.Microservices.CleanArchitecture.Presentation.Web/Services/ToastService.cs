using Net.Microservices.CleanArchitecture.Core.Application;
using Net.Microservices.CleanArchitecture.Infrastructure.Resources;
using Net.Microservices.CleanArchitecture.Presentation.Framework;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Hosting;
using Blazored.Toast.Configuration;

namespace Net.Microservices.CleanArchitecture.Presentation.Web.Services
{
    public class ToastService : IToastService
    {
        #region Private Members

        private readonly Blazored.Toast.Services.IToastService _blazoredService;
        private readonly ILocalizationService _localizer;

        #endregion

        #region Constructor

        public ToastService(ILocalizationService localizer,
            Blazored.Toast.Services.IToastService blazoredService) {
            _localizer = localizer;
            _blazoredService = blazoredService;
        }

        #endregion

        #region Public Methods
        public void ShowSuccess(string message)
        {
            var localizedSuccessMessage = _localizer[ResourceKeys.Notifications_Success];
            _blazoredService.ShowSuccess(message, localizedSuccessMessage.Value);
        }
        public void ShowError(string message) {
            _blazoredService.ShowError(message ?? _localizer[ResourceKeys.Common_SomethingWentWrong], _localizer[ResourceKeys.Notifications_Error]);
        }

        public void ShowError(RenderFragment message) {
            _blazoredService.ShowError(message);
        }

        public void ShowWarning(string message) {
            _blazoredService.ShowWarning(message, _localizer[ResourceKeys.Notifications_Warning]);
        }

        #endregion
    }
}
