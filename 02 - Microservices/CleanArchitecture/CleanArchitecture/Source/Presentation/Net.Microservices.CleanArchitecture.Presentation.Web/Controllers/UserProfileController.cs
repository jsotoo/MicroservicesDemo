﻿using System.Threading.Tasks;
using AutoMapper;
using Net.Microservices.CleanArchitecture.Core.Application;
using Net.Microservices.CleanArchitecture.Core.Application.Commands;
using Net.Microservices.CleanArchitecture.Infrastructure.Resources;
using Net.Microservices.CleanArchitecture.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Net.Microservices.CleanArchitecture.Presentation.Web.Controllers
{
    [Route("UserProfile")]
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IServiceBus _serviceBus;
        private readonly ILocalizationService _localizer;
        private readonly INotificationService _notificationService;
        private readonly IAuthenticatedUserService _userService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;
        private readonly IFileStorageService _fileStorageService;

        public UserProfileController(IServiceBus serviceBus,
            ILocalizationService localizer,
            INotificationService notificationService,
            IAuthenticatedUserService userService,
            IMapper mapper,
            IWebHostEnvironment webHost,
            IFileStorageService fileStorageService) {
            _serviceBus = serviceBus;
            _localizer = localizer;
            _notificationService = notificationService;
            _userService = userService;
            _mapper = mapper;
            _webHost = webHost;
            _fileStorageService = fileStorageService;
        }

        public IActionResult Index() {
            return View();
        }

        [Route("ChangePassword")]
        public IActionResult ChangePassword() {
            return PartialView("_ChangePassword");
        }

        [Route("ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model) {
            var changePasswordCommand = new ChangeUserPasswordCommand(_userService.Username, model.NewPassword);

            var result = await _serviceBus.Send(changePasswordCommand);

            if (result.Failed) {
                _notificationService.ErrorNotification(result.Message);
                return RedirectToAction(nameof(Index), new { t = "changepassword" });
            }

            _notificationService.SuccessNotification(_localizer[ResourceKeys.Notifications_PasswordChange_Success]);
            return RedirectToAction(nameof(Index), new { t = "changepassword" });
        }
    }
}
