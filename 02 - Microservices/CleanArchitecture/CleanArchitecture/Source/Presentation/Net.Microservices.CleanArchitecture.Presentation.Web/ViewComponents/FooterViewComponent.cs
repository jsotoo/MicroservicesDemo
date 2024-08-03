﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Presentation.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Net.Microservices.CleanArchitecture.Presentation.Web.ViewComponents
{
    [ViewComponent(Name = "Footer")]
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() {
            FooterViewModel model = new FooterViewModel();
            model.Date = DateTime.Now;
            model.Version = Assembly.GetEntryAssembly().GetName().Version.ToString(); //TODO[CH]: Get proper version here
            return View(model);
        }
    }
}
