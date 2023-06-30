
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcommerceShop.AdminApp.Controllers
{
    public class BaseController: Controller
    {
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            var sessions = context.HttpContext.Session.GetString("Token");
            if (sessions == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
            if(!User.Claims.Any(x => x.Type == "Role" && x.Value == "admin"))
            {
                context.Result = new ViewResult()
                {
                    ViewName = "NotAuthorized"
                };
            }
            base.OnActionExecuting(context);
        }
    }
}