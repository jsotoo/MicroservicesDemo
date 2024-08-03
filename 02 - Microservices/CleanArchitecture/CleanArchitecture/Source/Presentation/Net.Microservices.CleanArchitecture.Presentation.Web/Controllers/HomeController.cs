using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Net.Microservices.CleanArchitecture.Presentation.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index() {
            var model = new HomeViewModel();

            return View(model);
        }

    }
}