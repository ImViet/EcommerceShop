using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.AdminApp.Controllers
{
    public class ErrorController: BaseController
    {
        [HttpGet]
        public IActionResult NotFound()
        {
            return View();
        }
        [HttpGet]
        public IActionResult NotAuthorized()
        {
            return View("NotAuthorized");
        }
    }
}