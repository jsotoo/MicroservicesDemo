using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Net.Microservices.CleanArchitecture.Common;
using Net.Microservices.CleanArchitecture.Core.Application;
using Net.Microservices.CleanArchitecture.Core.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Net.Microservices.CleanArchitecture.Presentation.Web.Controllers
{
    [Route("Test")]
    public class TestController : Controller
    {
        private readonly IServiceBus _serviceBus;

        public TestController(IServiceBus serviceBus) {
            _serviceBus = serviceBus;
        }

        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser() {
            Result result = await _serviceBus.Send(
                new CreateUserCommand("dev", "Developer User", "dev@dev.dev", "12345678!!", new List<string>() { "SuperAdmin", "Admin" }));

            if (result.Succeeded) {
                return RedirectToAction("Index", "Home");
            }
            else {
                return NotFound($"{string.Join(',', result.Errors)}");
            }
        }

        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder() {
            Result result = await _serviceBus.Send(new CreateNewOrderCommand(Guid.NewGuid().ToString()));
            if (result.Succeeded) {
                return RedirectToAction("Index", "Home");
            }
            else {
                return NotFound($"{string.Join(',', result.Errors)}");
            }
        }

        [Route("AddOrderItem/{orderId}")]
        public async Task<IActionResult> AddOrderItem(string orderId)
        {            
            Result result = await _serviceBus.Send(new AddOrderItemCommand(new Guid(orderId), "Xbox",10,1));
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound($"{string.Join(',', result.Errors)}");
            }
        }
    }
}