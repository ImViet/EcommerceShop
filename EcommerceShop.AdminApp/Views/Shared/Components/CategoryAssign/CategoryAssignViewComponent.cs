using EcommerceShop.Contracts.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.AdminApp.Views.Shared.Components.CategoryAssign
{
    public class CategoryAssignViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CategoryAssignDto categories)
        {
            return View("Default", categories);
        }
    }
}