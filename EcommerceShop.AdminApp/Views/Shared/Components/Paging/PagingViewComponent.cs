using EcommerceShop.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.AdminApp.Views.Shared.Components.Paging
{
    public class PagingViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PagedResultBaseDto pagedResultBaseDto)
        {
            return View();
        }
    }
}