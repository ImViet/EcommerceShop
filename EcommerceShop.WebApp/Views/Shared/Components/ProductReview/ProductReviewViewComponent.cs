using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Views.Shared.Components.ProductReview
{
    public class ProductReviewViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            return View("Default");
        }
    }
}