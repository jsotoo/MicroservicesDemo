using Net.Microservices.CleanArchitecture.Core.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Net.Microservices.CleanArchitecture.Presentation.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly INotificationService NotificationService;

        public BaseController(INotificationService notificationService) {
            NotificationService = notificationService;
        }
    }
}
