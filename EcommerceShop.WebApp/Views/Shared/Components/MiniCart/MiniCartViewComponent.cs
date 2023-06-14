using EcommerceShop.WebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Views.Shared.Components.MiniCart
{
    public class MiniCartViewComponent: ViewComponent
    {
        private readonly ICartService _cartService;
        public MiniCartViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = _cartService.GetCart();
            return View("Default", cart);
        }
    }
}