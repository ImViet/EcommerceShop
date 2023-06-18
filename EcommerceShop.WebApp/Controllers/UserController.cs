using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Controllers
{
    public class UserController: Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}