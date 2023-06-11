using EcommerceShop.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.WebApp.Views.Shared.Components.Paging
{
    public class PagingViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PagedResultBaseDto pagedResultBaseDto)
        {
            return View();
        }
    }
}