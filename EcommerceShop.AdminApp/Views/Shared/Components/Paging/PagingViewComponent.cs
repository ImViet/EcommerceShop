using EcommerceShop.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceShop.AdminApp.Views.Shared.Components.Paging
{
    public class PagingViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(PagedResultBaseDto pagedResultBaseDto)
        {
            return View();
        }
    }
}