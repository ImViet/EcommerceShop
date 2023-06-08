using EcommerceShop.Contracts.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Views.Shared.Components.LastestProductHome
{
    public class LastestProductHomeViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<ProductDto> products)
        {
            return View("Default", products);
        }
    }
}