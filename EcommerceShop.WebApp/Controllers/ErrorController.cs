using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Controllers
{
    public class ErrorController: Controller
    {
        [HttpGet]
        public async Task<IActionResult> NotFound()
        {
            return View();
        }
    }
}